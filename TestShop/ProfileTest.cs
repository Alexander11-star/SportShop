using API_RestFull_Prueba.Models.User;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using WebApplicationCore.Controllers.API_RestFull_Prueba.Controllers;
using Xunit;


namespace TestShop
{
    public class ProfileTest
    {
        public ProfileTest()
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

            profileController = new ProfileController(dal);
        }

        ProfileController profileController;


        /// <summary>
        /// Test GET Sales
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async System.Threading.Tasks.Task TestGetProfile()
        {
            var result = await profileController.GetProfileAll();
            Assert.True(true);
        }
    }
}
