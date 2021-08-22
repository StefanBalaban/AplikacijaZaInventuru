using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ApplicationCore.Entities.UserAggregate;
using ApplicationCore.Extensions;
using PublicApi.Endpoints.UserEndpoints;
using Xunit;

namespace FunctionalTests.ApiTests.UserEndpoints
{
    [Collection("Sequential")]
    public class CreateEndpoint : IClassFixture<ApiTestFixture>
    {
        private readonly string _firstName = "Stefan";
        private readonly string _lastName = "Balaban";
        private readonly string _emailContact = "stefan.balaban@email.com";
        private JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        public CreateEndpoint(ApiTestFixture factory)
        {
            Client = factory.CreateClient();
        }

        public HttpClient Client { get; }

        [Fact]
        public async Task ReturnsNotAuthorizedGivenNormalUserToken()
        {
            var jsonContent = GetValidNewItemJson();
            var token = ApiTokenHelper.GetNormalUserToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await Client.PostAsync("api/User", jsonContent);

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task ReturnsSuccessGivenValidNewItemAndAdminUserToken()
        {
            var jsonContent = GetValidNewItemJson();
            var adminToken = ApiTokenHelper.GetAdminUserToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
            var response = await Client.PostAsync("api/User", jsonContent);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = stringResponse.FromJson<CreateUserResponse>();

            Assert.Equal(_firstName, model.User.FirstName);
        }

        private StringContent GetValidNewItemJson()
        {
            var request = new CreateUserRequest
            {
                FirstName = _firstName,
                LastName = _lastName,
                UserContactInfos = new List<UserContactInfo> { new UserContactInfo { Contact = _emailContact} }
             
            };
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            return jsonContent;
        }
    }
}