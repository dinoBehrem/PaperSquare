using PaperSquare.IntegrationTest.WebApplicationBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.IntegrationTest.System.API.Users.V_1
{
    public class UserControllerTest : IClassFixture<PaperSquareAppFactory<Program>>
    {
        private readonly HttpClient _client;
        private const string _path = "/api/user";

        public UserControllerTest(PaperSquareAppFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async void GetAll_GetsAllUsers()
        {
            // Arange

            const string _action = "/get-all";

            // TO DO: check pagination and filters

            // Act 

            var response = await _client.GetAsync(_path + _action);

            response.EnsureSuccessStatusCode();

            var content = response.Content;

            // Assert

            Assert.NotNull(response);
        }
    }
}
