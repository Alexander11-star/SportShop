using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SportShop.Common.Application;
using SportShop.Common.Application.MasterShop.Stores;
using SportShop.Model.Common;


namespace SportShop.Pages.Products.View
{
    public partial class ViewProduct
    {
        /// <summary>
        /// Servicio para llamar cajas de dialogo
        /// </summary>
        [Inject] public IDialogService DialogService { get; set; }
        /// <summary>
        /// Snackbar
        /// </summary>
        [Inject] ISnackbar Snackbar { get; set; }
        /// <summary>
        /// Store
        /// </summary>
        [Inject] ISportShop SportShop { get; set; }
        /// <summary>
        /// modelo
        /// </summary>
        [Parameter]
        public SportShopViewModel viewModel { get; set; }

        /// <summary>
        /// cantidad items
        /// </summary>
        private int totalItems;
        /// <summary>
        /// reload table
        /// </summary>
        public bool tableLoading { get; set; }
        /// <summary>
        /// Table
        /// </summary>
        private MudTable<Sales> table;
        /// <summary>
        /// vaiable upload
        /// </summary>
        private bool _processing = false;
        /// <summary>
        /// filtro tablas
        /// </summary>
        public string searchString { get; set; }
        /// <summary>
        /// paginacion periodos inductor
        /// </summary>
        private IEnumerable<Sales> pagedData;

        protected override void OnInitialized()
        {
            viewModel = new SportShopViewModel(SportShop);
            viewModel.Load();
            tableLoading = true;
        }

        /// <summary>
        /// Inicializar asincronico
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await viewModel.GetSales();

        }

        private async Task<TableData<Sales>> ServerReload(TableState state)
        {
            
            await Processing(true);
            tableLoading = true;
            StateHasChanged();

            await viewModel.GetSales();

            IEnumerable<Sales> data = viewModel.ObjectsSalesGroup.Where(x => string.IsNullOrEmpty(searchString) || x.NameSProduct.ToLower().Contains(searchString.ToLower()) || x.DescriptionSProduct.ToLower().Contains(searchString.ToLower()) || x.Quantity.ToString().Contains(searchString.ToString()) || x.CostSProduct.ToString().Contains(searchString.ToString())).ToList();
            tableLoading = false;
            StateHasChanged();
            totalItems = data.Count();

            switch (state.SortLabel)
            {
                case "so_NameProduct":
                    data = data.OrderByDirection(state.SortDirection, o => o.NameSProduct);
                    break;
                case "cco_DescriptionSProduct":
                    data = data.OrderByDirection(state.SortDirection, o => o.DescriptionSProduct);
                    break;
                case "cco_QuantityProduct":
                    data = data.OrderByDirection(state.SortDirection, o => o.Quantity);
                    break;
                case "cco_CostProduct":
                    data = data.OrderByDirection(state.SortDirection, o => o.CostSProduct);
                    break;
            }

            if (searchString == null || searchString.ToString().Length == 0)
            {
                pagedData = data.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();
            }
            else
            {
                state.Page = 0;
                pagedData = data.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();
            }

            await Processing(false);
            return new TableData<Sales>() { TotalItems = totalItems, Items = pagedData };
        }

        /// <summary>
        /// Asigna valor a texto para busqueda en la tabla
        /// </summary>
        /// <param name="text"></param>
        private void OnSearch(string text)
        {
            searchString = text;
            table.ReloadServerData();
        }

        /// <summary>
        /// loading del boton
        /// </summary>
        /// <returns></returns>
        async Task Processing(bool state)
        {
            _processing = state;
            await Task.Delay(3);
            StateHasChanged();

        }

        /// <summary>
        /// Adicionar nuevo registro
        /// </summary>
        public async Task AddRegister()
        {
            await ShowDialog("CreateProductDialog");
        }

        public async Task ShowDialog(string dialogName, object parameter = null)
        {

            switch (dialogName)
            {            
                case "CreateProductDialog":
                    var resulcreate = await DialogService.Show<CreateProductDialog>(string.Empty,
                        new DialogParameters { ["SportShopViewModel"] = viewModel },
                        new DialogOptions() { DisableBackdropClick = true, MaxWidth = MaxWidth.ExtraLarge, FullWidth = true }).Result;
                    if (resulcreate != null && !resulcreate.Cancelled)
                    {
                        await table.ReloadServerData();
                        StateHasChanged();
                    }
                    break;

            }
        }
    }
}
