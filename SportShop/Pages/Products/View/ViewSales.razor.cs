using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using SportShop.Common.Application;
using SportShop.Common.Application.MasterShop.Stores;
using SportShop.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace SportShop.Pages.Products.View
{
    public partial class ViewSales
    {
        /// <summary>
        /// WebHostEnvironment
        /// </summary>
        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; }
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
        /// propedad para validad los campos vacios
        /// </summary>
        public bool ValidateSaveSales { get; set; } = true;
        /// <summary>
        /// vaiable upload
        /// </summary>
        private bool _processing = false;
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
        /// filtro tablas
        /// </summary>
        public string searchString { get; set; }
        /// <summary>
        /// paginacion periodos inductor
        /// </summary>
        private IEnumerable<Sales> pagedData;

        #region methods

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
            await viewModel.GetProducts();
            await viewModel.GetSales();

        }

        /// <summary>
        /// Guarda relacion de Centro de Costos
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync()
        {
            ///Realiza validacion de campos vacios
            ValidateSave();

            if (ValidateSaveSales)
            {
                await Processing(true);
                await viewModel.SaveSaleAsync();
                ValidatemessageSave();
                //limpia valores
                viewModel.ClearSelectedValues();
                await table.ReloadServerData();
                await Processing(false);
                StateHasChanged();
            }
        }

        /// <summary>
        /// loading del boton
        /// </summary>
        /// <returns></returns>
        async Task Processing(bool state)
        {
            _processing = state;
            await Task.Delay(4);
            StateHasChanged();

        }

        /// <summary>
        /// Valida el mensaje de confirmacion cuando se guarda o edita un registro
        /// </summary>
        public void ValidatemessageSave()
        {
            Snackbar.Add("Registro guardado exitosamente!", Severity.Success);            
        }

        public void ValidateSave()
        {
            ValidateSaveSales = true;

            if (string.IsNullOrEmpty(viewModel.IdProduct) || viewModel.QuantityProduct == 0 || viewModel.DateSales == null)
            {
                ValidateSaveSales = false;

                Snackbar.Add("Señor usuario, Por favor diligenciar todos los campos.", Severity.Info);
            }   
        }

        private async Task<TableData<Sales>> ServerReload(TableState state)
        {

            await Processing(true);
            tableLoading = true;
            StateHasChanged();
            await viewModel.GetSales();

            IEnumerable<Sales> data = viewModel.ObjectsSales.Where(x => string.IsNullOrEmpty(searchString) || x.NameSProduct.ToLower().Contains(searchString.ToLower())  || x.DescriptionSProduct.ToLower().Contains(searchString.ToLower()) || x.CostSProduct.ToString().Contains(searchString.ToString()) || x.NameUser.ToLower().Contains(searchString.ToLower()) || x.DateSales.ToString().Contains(searchString.ToString()) || x.Quantity.ToString().Contains(searchString.ToString())).ToList();
            tableLoading = false;
            totalItems = data.Count();

            switch (state.SortLabel)
            {
                case "so_NameProduct":
                    data = data.OrderByDirection(state.SortDirection, o => o.NameSProduct);
                    break;
                case "cco_DescriptionSProduct":
                    data = data.OrderByDirection(state.SortDirection, o => o.DescriptionSProduct);
                    break;
                case "cco_CostSProduct":
                    data = data.OrderByDirection(state.SortDirection, o => o.CostSProduct);
                    break;
                case "cco_NameUser":
                    data = data.OrderByDirection(state.SortDirection, o => o.NameUser);
                    break;
                case "cco_DateSales":
                    data = data.OrderByDirection(state.SortDirection, o => o.DateSales);
                    break;
                case "cco_Quantity":
                    data = data.OrderByDirection(state.SortDirection, o => o.Quantity);
                    break;
                case "cco_CostSProductT":
                    data = data.OrderByDirection(state.SortDirection, o => (o.Quantity*o.CostSProduct));
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
        /// Elimina relacion de Centro de Costos por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task ButtonDeleteClicked(int id)
        {
            await Processing(true);

            var resultDelete = await viewModel.DeleteSales(id);

            if (resultDelete > 0)
            {
                Snackbar.Add("Se ha borrado el registro!", Severity.Normal);
                await table.ReloadServerData();
                await Processing(false);
                StateHasChanged();
            }
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
        /// Carga de Archivo
        /// </summary>
        private async Task UploadFiles(InputFileChangeEventArgs e)
        {
            foreach (var file in e.GetMultipleFiles())
            {
                try
                {
                    var filePath = Path.Combine(WebHostEnvironment.WebRootPath, "Upload", file.Name);
                    using (var ms = file.OpenReadStream())
                    {
                        //using (Stream destination = File.Create(filePath))
                        //{
                        //    await ms.CopyToAsync(destination);
                        //}
                    }
                    viewModel.ImportFile(filePath);
                }
                catch (Exception ex)
                {
                    throw ex.InnerException;
                }
            }
        }

        /// <summary>
        /// Carga centros de costo origen por estructuras administrativa origen seleccionada
        /// </summary>
        /// <returns></returns>
        public async Task LoadDescription()
        {
            await viewModel.LoadDescription();
        }

        #endregion
    }
}
