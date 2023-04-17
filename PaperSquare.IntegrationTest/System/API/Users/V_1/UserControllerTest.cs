using Microsoft.AspNetCore.WebUtilities;
using PaperSquare.Infrastructure.Features.UserManagement.Dto;
using PaperSquare.IntegrationTest.WebApplicationBuilder;
using System.Net;
using System.Net.Http.Json;

namespace PaperSquare.IntegrationTest.System.API.Users.V_1
{
    public class UserControllerTest : IClassFixture<PaperSquareAppFactory<Program>>
    {
        private readonly HttpClient _client;
        private const string PATH = "/api/user";

        public UserControllerTest(PaperSquareAppFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        #region GetAll

        [Fact]
        public async void GetAll_GetsAllUsers_ResturnsStatus200()
        {
            // Arange

            const string ACTION = "/get-all";

            var page = 1;
            var pageSize = 1;

            var queryParameters = new Dictionary<string, string>()
            {
                ["FirstName"] = "First name -- 1",
                ["LastName"] = "Last name -- 1",
                ["BirthDate "] = DateTime.Now.ToString("f"),
                ["Page"] = page.ToString(),
                ["PageSize"] = pageSize.ToString()
            };
            
            var endpoint = QueryHelpers.AddQueryString(PATH + ACTION, queryParameters);

            // Act 

            var response = await _client.GetAsync(endpoint);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadFromJsonAsync<IEnumerable<UserDto>>();

            // Assert

            Assert.NotNull(response);
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            Assert.NotNull(content);
            Assert.True(content?.Count() <= pageSize);
        }

        #endregion GetAll

        #region GetById

        [Fact]
        public async void GetById_ValidId_ReturnsUserWithStatus200()
        {
            // Arrange 

            const string ACTION = "/get-by-id";

            var queryParameters = new Dictionary<string, string>()
            {
                ["id"] = "user-1-id",
            };

            var endpoint = QueryHelpers.AddQueryString(PATH + ACTION, queryParameters);

            // Act 

            var response = await _client.GetAsync(endpoint);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadFromJsonAsync<UserDto>();

            // Assert

            Assert.NotNull(response);
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            Assert.NotNull(content);
        }

        [Fact]
        public async void GetById_InvalidId_ReturnsNotFoundResult404()
        {
            // Arrange 

            const string ACTION = "/get-by-id";

            var queryParameters = new Dictionary<string, string>()
            {
                ["id"] = "user1id",
            };

            var endpoint = QueryHelpers.AddQueryString(PATH + ACTION, queryParameters);

            var responseMessage = "User not found!";

            // Act 

            var response = await _client.GetAsync(endpoint);

            var content = await response.Content.ReadAsStringAsync();

            // Assert

            Assert.NotNull(response);
            Assert.True(response.StatusCode == HttpStatusCode.NotFound);
            Assert.NotNull(content);
            Assert.True(content.Contains(responseMessage));
        }

        #endregion GetById

        #region Insert

        [Fact]
        public async void Insert_ValidData_ReturnsUserDtoWithStatus201()
        {
            // Arrange

            const string ACTION = "/insert";

            var requestBody = new Dictionary<string, string>()
            {
                ["firstname"] = "John",
                ["lastname"] = "Doe",
                ["email"] = "johnDoe@email.com",
                ["username"] = "johnDoe",
                ["password"] = "johnDoe1!",
                ["confirmPassword"] = "johnDoe1!"
            };

            var httpContent = JsonContent.Create(requestBody);

            // Act 

            var response = await _client.PostAsync(PATH + ACTION, httpContent);

            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadFromJsonAsync<UserDto>();

            // Assert

            Assert.NotNull(content);
            Assert.True(response.StatusCode == HttpStatusCode.Created);
            Assert.NotNull(content);
        }
        
        [Fact]
        public async void Insert_InvalidData_ReturnsErrorWithStatus400()
        {
            // Arrange

            const string ACTION = "/insert";

            var requestBody = new Dictionary<string, string>()
            {
                ["firstname"] = "John",
                ["lastname"] = "Doe",
                ["email"] = "johnDoe@email.com",
                ["username"] = "johnDoe",
                ["password"] = "johnDoe1!",
                ["confirmPassword"] = "johnDoe!1"
            };

            var httpContent = JsonContent.Create(requestBody);

            const string errorMessage = "Passwords doesn`t match!";

            // Act 

            var response = await _client.PostAsync(PATH + ACTION, httpContent);

            var content = await response.Content.ReadAsStringAsync();

            // Assert

            Assert.NotNull(content);
            Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
            Assert.NotNull(content);
            Assert.Contains(errorMessage, content);
        }
        
        #endregion Insert

        #region Update

        [Fact]
        public async void Update_ValidData_ReturnsUserDtoWithStatus200()
        {
            // Arrange

            const string ACTION = "/update";

            var requestBody = new Dictionary<string, string>()
            {
                ["firstName"] =  "Johny",
                ["lastName"] = "Doe",
                ["email"] = "johny.doe@email.com"
            };

            var httpContent = JsonContent.Create(requestBody);

            var userId = new Dictionary<string, string>()
            {
                ["id"] = "user-1-id"
            };
        
            var endpoint = QueryHelpers.AddQueryString(PATH + ACTION, userId);

            EnsureAuthorizationIsAdded();

            // Act

            var response = await _client.PostAsync(endpoint, httpContent);

            response.EnsureSuccessStatusCode();

            // Assert

            Assert.NotNull(response);
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }

        [Fact]
        public async void Update_InvalidUserId_ReturnsErrorsWithStatus404()
        {
            // Arrange 

            const string ACTION = "/update";

            var requestBody = new Dictionary<string, string>()
            {
                ["firstName"] = "Johny",
                ["lastName"] = "Doe",
                ["email"] = "johny.doe@email.com"
            };

            var httpContent = JsonContent.Create(requestBody);

            var userId = new Dictionary<string, string>()
            {
                ["id"] = "user-2-id"
            };

            var endpoint = QueryHelpers.AddQueryString(PATH + ACTION, userId);

            EnsureAuthorizationIsAdded();

            // Act

            var response = await _client.PostAsync(endpoint, httpContent);

            // Arrange

            Assert.NotNull(response);
            Assert.True(response.StatusCode == HttpStatusCode.NotFound);
        }

        #endregion Update

        #region Delete

        [Fact]
        public async void Delete_ValidUser_ReturnsUserDtoWithStatus200()
        {
            // Arrange 

            const string ACTION = "/delete";

            var userId = new Dictionary<string, string>()
            {
                ["id"] = "user-2-id"
            };

            var endpoint = QueryHelpers.AddQueryString(PATH + ACTION, userId);

            EnsureAuthorizationIsAdded();

            // Act

            var response = await _client.DeleteAsync(endpoint);

            response.EnsureSuccessStatusCode();

            // Assert

            Assert.NotNull(response);
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }
        
        
        [Fact]
        public async void Delete_InvalidUser_ReturnsErrorsWithStatus404()
        {
            // Arrange 

            const string ACTION = "/delete";

            var userId = new Dictionary<string, string>()
            {
                ["id"] = "user-102-id"
            };

            var endpoint = QueryHelpers.AddQueryString(PATH + ACTION, userId);

            EnsureAuthorizationIsAdded();

            // Act

            var response = await _client.DeleteAsync(endpoint);

            // Assert

            Assert.NotNull(response);
            Assert.True(response.StatusCode == HttpStatusCode.NotFound);
        }

        #endregion Delete

        #region Utils

        private void EnsureAuthorizationIsAdded()
        {
            _client.DefaultRequestHeaders.Add("Authorization", new List<string>() { "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6InVzZXItMS1pZCIsIlVzZXJOYW1lIjoidXNlck5hbWUtMSIsIkVtYWlsIjoidGVzdHVzZXIxQGV4YW1wbGUuY29tIiwiUm9sZSI6WyJBZG1pbiIsIlJlZ2lzdGVyZWRVc2VyIl0sImV4cCI6MTgzODE5NjM2NH0.ydOwF-sCooT48_YfITAewv4UhkKYBrc4CZJS_YDatnA" });
        }

        #endregion Utils
    }
}