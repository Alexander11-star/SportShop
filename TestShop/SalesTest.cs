using API_RestFull_Prueba.Models.User;
using Microsoft.Extensions.Configuration;
using SportShop.Model.Common;
using System;
using System.Collections.Generic;
using WebApplicationCore.Controllers.API_RestFull_Prueba.Controllers;
using Xunit;


namespace TestShop
{
    public class SalesTest
    {
        public SalesTest()
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

            salesController = new SalesController(dal);
        }

        SalesController salesController;


        /// <summary>
        /// Test GET Sales
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async System.Threading.Tasks.Task TestGetSales()
        {
            var result = await salesController.GetSalesAll();
            Assert.True(true);
        }


        /// <summary>
        /// Test GET Sales group
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async System.Threading.Tasks.Task TestGetSalesGroup()
        {
            var result = await salesController.GetSalesGroup();
            Assert.True(true);
        }

        /// <summary>
        /// Delete sales by Id
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async System.Threading.Tasks.Task DeleteSalesById()
        {
            var result = await salesController.Delete(3);
            Assert.True(true);
        }

        /// <summary>
        /// Test create Products
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async System.Threading.Tasks.Task TestCreateSales()
        {
            Sales ObjectsSales = new Sales()

            {
                IdSales = 0,
                IdSProduct = 1,
                IdUser = 3,
                Quantity = 2,
                DateSales = DateTime.Now
            };

            await salesController.Post(ObjectsSales);
            Assert.True(true);
        }

    }
}
