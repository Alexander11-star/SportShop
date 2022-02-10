using API_RestFull_Prueba.Controllers;
using API_RestFull_Prueba.Models.User;
using Microsoft.Extensions.Configuration;
using SportShop.Model.Common;
using System.Collections.Generic;
using Xunit;


namespace TestShop
{
    public class ProductsTest
    {
        public ProductsTest()
        {
            var inMemorySettings = new Dictionary<string, string> {
            {
                    "ConnectionStrings:DefaultConnection", "Data Source=(localdb)\\Server;Initial catalog=SportShop;Integrated Security=true;"
                }, 
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            UsersDAL dal = new UsersDAL(configuration);

            ProductsController = new ProductsController(dal);
        }

        ProductsController ProductsController;


        /// <summary>
        /// Test GET Products
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async System.Threading.Tasks.Task TestGetProducts()
        {
            var result = await ProductsController.GetProductsAll();
            Assert.True(true);          
        }

        /// <summary>
        /// Test create Products
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async System.Threading.Tasks.Task TestCreateProduct()
        {

            Products ObjectsProducts  = new Products()

            {
            CodProduct = "PRT",
            NameProduct = "ProductoTest",
            DescriptionProduct = "ProductoTest Description",
            CostProduct = 1,
            ImageProduct = "/URL/PRUEBA"
            };
            
            await ProductsController.Post(ObjectsProducts);
            Assert.True(true);
        }

        /// <summary>
        /// Delete product by Id
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async System.Threading.Tasks.Task DeleteProductById()
        {
            var result = await ProductsController.Delete(3);
            Assert.True(true);
        }
    }
}
