using Microsoft.AspNetCore.WebUtilities;
using PaperSquare.API.Feature.Auth.Dto;
using PaperSquare.Core.Models.Identity;
using PaperSquare.Infrastructure.Features.UserManagement.Dto;
using PaperSquare.IntegrationTest.WebApplicationBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xunit.Sdk;

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

        #region GetAll

        [Fact]
        public async void GetAll_GetsAllUsers_ResturnsStatus200()
        {
            // Arange

            const string _action = "/get-all";

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
            
            var endpoint = QueryHelpers.AddQueryString(_path + _action, queryParameters);

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

            const string _action = "/get-by-id";

            var queryParameters = new Dictionary<string, string>()
            {
                ["id"] = "user-1-id",
            };

            var endpoint = QueryHelpers.AddQueryString(_path + _action, queryParameters);

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

            const string _action = "/get-by-id";

            var queryParameters = new Dictionary<string, string>()
            {
                ["id"] = "user1id",
            };

            var endpoint = QueryHelpers.AddQueryString(_path + _action, queryParameters);

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

            const string _action = "/insert";

            var requestBody = new Dictionary<string, string>()
            {
                { "firstname" , "John" },
                { "lastname" , "Doe" },
                { "email" , "johnDoe@email.com" },
                { "username" , "johnDoe" },
                { "password" , "johnDoe1!" },
                { "confirmPassword" , "johnDoe1!" }
            };

            var httpContent = JsonContent.Create(requestBody);

            // Act 

            var response = await _client.PostAsync(_path + _action, httpContent);

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

            const string _action = "/insert";

            var requestBody = new Dictionary<string, string>()
            {
                { "firstname" , "John" },
                { "lastname" , "Doe" },
                { "email" , "johnDoe@email.com" },
                { "username" , "johnDoe" },
                { "password" , "johnDoe1!" },
                { "confirmPassword" , "johnDoe!1" }
            };

            var httpContent = JsonContent.Create(requestBody);

            const string errorMessage = "Passwords doesn`t match!";

            // Act 

            var response = await _client.PostAsync(_path + _action, httpContent);

            var content = await response.Content.ReadAsStringAsync();

            // Assert

            Assert.NotNull(content);
            Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
            Assert.NotNull(content);
            Assert.Contains(errorMessage, content);
        }
        
        //[Fact]
        //public async void Insert_InvalidData_ReturnsInternalServerErrorWithStatus500()
        //{
        //    // Arrange

        //    const string _action = "/insert";

        //    var requestBody = new Dictionary<string, string>()
        //    {
        //        { "firstname" , "John" },
        //        { "lastname" , "Doe" },
        //        { "email" , "johnDoe@email.com" },
        //        { "username" , "johnDoe" },
        //        { "password" , "johnDoe1!" },
        //        { "confirmPassword" , "johnDoe1!" }
        //    };

        //    var httpContent = JsonContent.Create(requestBody);

        //    // Act 

        //    var response = await _client.PostAsync(_path + _action, httpContent);

        //    var content = await response.Content.ReadAsStringAsync();

        //    // Assert

        //    Assert.NotNull(content);
        //    Assert.True(response.StatusCode == HttpStatusCode.InternalServerError);
        //}

        #endregion Insert

        #region Update

        [Fact]
        public async void Update_ValidData_ReturnsUserDtoWithStatus200()
        {
            // Arrange

            const string _action = "/update";

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
        
            var endpoint = QueryHelpers.AddQueryString(_path + _action, userId);

            _client.DefaultRequestHeaders.Add("Authorization", new List<string>() { "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6InVzZXItMS1pZCIsIlVzZXJOYW1lIjoidXNlck5hbWUtMSIsIkVtYWlsIjoidGVzdHVzZXIxQGV4YW1wbGUuY29tIiwiUm9sZSI6WyJBZG1pbiIsIlJlZ2lzdGVyZWRVc2VyIl0sImV4cCI6MTgzODE5NjM2NH0.ydOwF-sCooT48_YfITAewv4UhkKYBrc4CZJS_YDatnA" });

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

            const string _action = "/update";

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

            var endpoint = QueryHelpers.AddQueryString(_path + _action, userId);

            _client.DefaultRequestHeaders.Add("Authorization", new List<string>() { "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6InVzZXItMS1pZCIsIlVzZXJOYW1lIjoidXNlck5hbWUtMSIsIkVtYWlsIjoidGVzdHVzZXIxQGV4YW1wbGUuY29tIiwiUm9sZSI6WyJBZG1pbiIsIlJlZ2lzdGVyZWRVc2VyIl0sImV4cCI6MTgzODE5NjM2NH0.ydOwF-sCooT48_YfITAewv4UhkKYBrc4CZJS_YDatnA" });

            // Act

            var response = await _client.PostAsync(endpoint, httpContent);

            // Arrange

            Assert.NotNull(response);
            Assert.True(response.StatusCode == HttpStatusCode.NotFound);
        }

        #endregion Update
    }
}