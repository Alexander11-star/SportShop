using Microsoft.AspNetCore.Components;
using MudBlazor;
using SportShop.Common.Application;
using SportShop.Common.Application.MasterShop.Stores;
using System.Linq;
using System.Threading.Tasks;

namespace SportShop.Pages
{
    public partial class Index
    {
        /// <summary>
        /// Navegacion
        /// </summary>
        [Inject] NavigationManager navigation { get; set; }
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
        public string TitleHead { get; set; } = "HOME";
        protected override void OnInitialized()
        {
            viewModel = new SportShopViewModel(SportShop);
        }

        /// <summary>
        /// Inicializar asincronico
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await viewModel.GetUsers();
        }

        /// <summary>
        /// Valida si existe el usuario y la contraseña
        /// </summary>
        /// <returns></returns>
        public void ValidatonLogin()
        {
            if (viewModel.ValidateLogin())
            {
                viewModel.LoginAutorized = true;
                viewModel.IdprofileAccess = viewModel.ObjectsUser.FirstOrDefault(x => x.Name.ToLower().Equals(viewModel.User.ToLower()) && x.Password.ToLower().Equals(viewModel.Password.ToLower())).IdProfile;
            }
            else
            {
                Snackbar.Add("Señor usuario, El usuario y la contraseña ingresada son incorrectos.", Severity.Info);
            }
        }
        public void OnNavlinkClick(string component, string componentName)
        {
            TitleHead = componentName;
            navigation.NavigateTo(component);
        }

    }
}
