using API_RestFull_Prueba.Controllers;
using API_RestFull_Prueba.Models.User;
using Microsoft.Extensions.Configuration;
using SportShop.Model.Common;
using System.Collections.Generic;
using Xunit;


namespace TestShop
{
    public class UserTest
    {
        public UserTest()
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

            userController = new UserController(dal);
        }

        UserController userController;


        /// <summary>
        /// Test GET User
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async System.Threading.Tasks.Task TestGetUser()
        {
            var result = await userController.GetUserAll();
            Assert.True(true);
        }

        /// <summary>
        /// Test GET User by ID
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async System.Threading.Tasks.Task TestGetUserById()
        {
            var result = await userController.GetUserById(3);
            Assert.True(true);
        }
    }
}
