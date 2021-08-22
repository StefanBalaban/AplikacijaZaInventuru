using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PublicApi.Endpoints.UserWeightEvidentionEndpoints;
using ApplicationCore.Extensions;
using Xunit;

namespace FunctionalTests.ApiTests.UserWeightEvidentionWeightEvidentationEndpoints
{
    [Collection("Sequential")]
    public class CreateEndpoint : IClassFixture<ApiTestFixture>
    {
        private readonly DateTime _date = new DateTime(2002, 2, 2);
        private readonly float _weight = 12.5f;
        private JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        public CreateEndpoint(ApiTestFixture factory)
        {
            Client = factory.CreateClient();
        }

        public HttpClient Client { get; }

        [Fact]
        public async Task ReturnsNotAuthorizedGivenNormalUserWeightEvidentionToken()
        {
            var jsonContent = GetValidNewItemJson();
            var token = ApiTokenHelper.GetNormalUserToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await Client.PostAsync("api/UserWeightEvidention", jsonContent);

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task ReturnsSuccessGivenValidNewItemAndAdminUserWeightEvidentionToken()
        {
            var jsonContent = GetValidNewItemJson();
            var adminToken = ApiTokenHelper.GetAdminUserToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
            var response = await Client.PostAsync("api/UserWeightEvidention", jsonContent);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = stringResponse.FromJson<CreateUserWeightEvidentionResponse>();

            Assert.Equal(_weight, model.UserWeightEvidention.Weight);
            Assert.Equal(_date, model.UserWeightEvidention.EvidentationDate);
        }

        private StringContent GetValidNewItemJson()
        {
            var request = new CreateUserWeightEvidentionRequest
            {
                Weight = _weight,
                EvidentationDate = _date
             
            };
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            return jsonContent;
        }
    }
}