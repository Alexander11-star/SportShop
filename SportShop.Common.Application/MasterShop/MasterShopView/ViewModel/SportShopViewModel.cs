using SportShop.Common.Application.MasterShop.Stores;
using SportShop.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SCSE.Framework.Common;
using System.Linq;

namespace SportShop.Common.Application
{
    public class SportShopViewModel
    {
        private readonly ISportShop _SportStore;
        public SportShopViewModel(ISportShop SportStore)
        {
            _SportStore = SportStore;
        }

        public int IdprofileAccess { get; set; }
        /// <summary>
        /// Login sesion
        /// </summary>
        public bool LoginAutorized { get; set; } = false;
        /// <summary>
        /// Usuario
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// imagen
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// cantidad
        /// </summary>
        public int QuantityProduct { get; set; }
        /// <summary>
        /// descripccion
        /// </summary>
        public string DescriptionProduct { get; set; }
        /// <summary>
        /// Usuario
        /// </summary>
        public DateTime? DateSales { get; set; }
        /// <summary>
        /// Contraseña
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Objeto productos
        /// </summary>
        public Products ObjectsProducts { get; set; } = new Products();
        /// <summary>
        /// Listado generico de Ventas
        /// </summary>
        public List<Products> ListProducts { get; set; } = new List<Products>();
        /// <summary>
        /// Listado generico de Ventas
        /// </summary>
        public List<Products> ListProductsFilter { get; set; } = new List<Products>();
        /// <summary>
        /// Listado generico de Ventas
        /// </summary>
        public List<Sales> ObjectsSales { get; set; } = new List<Sales>();
        /// <summary>
        /// Listado generico de Ventas
        /// </summary>
        public List<Sales> ObjectsSalesGroup { get; set; } = new List<Sales>();
        /// <summary>
        /// Id producto
        /// </summary>
        public string IdProduct { get; set; }
        /// <summary>
        /// Listado generico de Ventas
        /// </summary>
        public Sales SalesProduct { get; set; }
        /// <summary>
        /// Listado generico de Ventas
        /// </summary>
        public List<Users> ObjectsUser { get; set; }

        /// <summary>
        /// Lista de ventas
        /// </summary>
        /// <param name="Active"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<List<Sales>> GetSales()
        {
            ObjectsSales = await _SportStore.GetSales();

            ObjectsSalesGroup = await _SportStore.GetSalesGroup();

            foreach (var item in ObjectsSalesGroup)
            {
                item.NameSProduct = ObjectsSales.FirstOrDefault(x => x.IdSProduct == item.IdSProduct).NameSProduct;
                item.DescriptionSProduct = ObjectsSales.FirstOrDefault(x => x.IdSProduct == item.IdSProduct).DescriptionSProduct;
                var cost = ObjectsSales.FirstOrDefault(x => x.IdSProduct == item.IdSProduct).CostSProduct;
                var cant = ObjectsSales.FirstOrDefault(x => x.IdSProduct == item.IdSProduct).Quantity;
                item.CostSProduct = cost * cant;
            }

            return ObjectsSales;
        }

        /// <summary>
        /// Listado de Usuario
        /// </summary>
        /// <param name="Active"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<List<Users>> GetUsers()
        {
            ObjectsUser = await _SportStore.GetUsers();

            return ObjectsUser;
        }

        /// <summary>
        /// Inicializa Listados
        /// </summary>
        public void Load()
        {
            ObjectsSales = new List<Sales>();
        }

        /// <summary>
        /// Valida si existe usuario y password
        /// </summary>
        /// <returns></returns>
        public Boolean ValidateLogin()
        {
            return ObjectsUser.Any(x => x.Name.ToLower().Equals(User.ToLower()) && x.Password.ToLower().Equals(Password.ToLower()));
        }

        /// <summary>
        /// Lista de productos
        /// </summary>
        /// <param name="Active"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<List<Products>> GetProducts()
        {
            ListProducts = await _SportStore.GetProducts();
            ListProductsFilter = ListProducts;
            return ListProducts;
        }

        /// <summary>
        /// Graba las ventas
        /// </summary>
        /// <returns></returns>
        public async Task SaveSaleAsync()
        {

            Sales sale = new Sales();

            sale.IdSales = 0;
            sale.IdSProduct = Convert.ToInt32(IdProduct);
            sale.IdUser = 3;
            sale.Quantity = QuantityProduct;
            sale.DateSales = (DateTime)DateSales;

            await _SportStore.Save(sale);
        }

        /// <summary>
        /// Graba los productos
        /// </summary>
        /// <returns></returns>
        public async Task SaveProducts()
        {
            await _SportStore.SaveProducts(ObjectsProducts);
        }

        /// <summary>
        /// Carga la descripcion
        /// </summary>
        /// <returns></returns>
        public async Task LoadDescription()
        {
            DescriptionProduct = ListProducts.FirstOrDefault(x => x.IdProduct.ToString().Equals(IdProduct)).DescriptionProduct;
        }

        /// <summary>
        /// Limpia valores
        /// </summary>
        public void ClearSelectedValues()
        {
            IdProduct = string.Empty;
            QuantityProduct = 0;
            DateSales = null;
            ObjectsProducts = new Products();
            DescriptionProduct = string.Empty;
        }

        /// <summary>
        /// elimina valores del venta
        /// </summary>
        /// <returns></returns>
        public async Task<int> DeleteSales(int id)
        {
            return await _SportStore.DeleteSales(id);
        }

        /// <summary>
        /// elimina producto por Id
        /// </summary>
        /// <returns></returns>
        public async Task<int> DeleteProduct(int id)
        {
            return await _SportStore.DeleteProduct(id);
        }

        /// <summary>
        /// Importación de Archivos
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public void ImportFile(string file)
        {
            try
            {
                ObjectsProducts.ImageProduct = file.Substring(0,25);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
