using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using ApplicationCore.Extensions;
using PublicApi.Endpoints.UserWeightEvidentionEndpoints;
using Xunit;

namespace FunctionalTests.ApiTests.UserWeightEvidentionWeightEvidentationEndpoints
{
    [Collection("Sequential")]
    public class GetByIdEndpoint : IClassFixture<ApiTestFixture>
    {
        private JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        public GetByIdEndpoint(ApiTestFixture factory)
        {
            Client = factory.CreateClient();
        }

        public HttpClient Client { get; }

        [Fact]
        public async Task ReturnsItemGivenValidId()
        {
            var adminToken = ApiTokenHelper.GetAdminUserToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
            var response = await Client.GetAsync("api/UserWeightEvidention/5");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = stringResponse.FromJson<GetByIdUserWeightEvidentionResponse>();

            Assert.Equal(5, model.UserWeightEvidention.Id);
            Assert.Equal(new DateTime(2001, 1, 1), model.UserWeightEvidention.EvidentationDate);           
            Assert.Equal(12, model.UserWeightEvidention.Weight);
        }

        [Fact]
        public async Task ReturnsNotFoundGivenInvalidId()
        {
            var adminToken = ApiTokenHelper.GetAdminUserToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
            var response = await Client.GetAsync("api/UserWeightEvidention/0");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}