using Microsoft.AspNetCore.Components;
using MudBlazor;
using SportShop.Common.Application;
using SportShop.Common.Application.MasterShop.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportShop.Shared
{
    public partial class NavMenu
    {
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
        protected override void OnInitialized()
        {
            viewModel = new SportShopViewModel(SportShop);
        }
    }
}
