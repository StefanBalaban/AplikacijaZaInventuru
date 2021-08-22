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
    public class DeleteEndpoint : IClassFixture<ApiTestFixture>
    {
        private JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        public DeleteEndpoint(ApiTestFixture factory)
        {
            Client = factory.CreateClient();
        }

        public HttpClient Client { get; }

        [Fact]
        public async Task ReturnsSuccessGivenValidIdAndAdminUserWeightEvidentionToken()
        {
            var adminToken = ApiTokenHelper.GetAdminUserToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
            var response = await Client.DeleteAsync("api/UserWeightEvidention/12");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = stringResponse.FromJson<DeleteUserWeightEvidentionResponse>();

            Assert.Equal("Deleted", model.Status);
        }

        [Fact]
        public async Task ReturnsNotFoundGivenInvalidIdAndAdminUserWeightEvidentionToken()
        {
            var adminToken = ApiTokenHelper.GetAdminUserToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
            var response = await Client.DeleteAsync("api/UserWeightEvidention/0");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}