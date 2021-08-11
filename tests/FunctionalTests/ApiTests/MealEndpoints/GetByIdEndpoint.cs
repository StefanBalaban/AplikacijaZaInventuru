using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using ApplicationCore.Extensions;
using PublicApi.Endpoints.MealEndpoints;
using Xunit;

namespace FunctionalTests.ApiTests.MealEndpoints
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
            var response = await Client.GetAsync("api/Meal/5");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = stringResponse.FromJson<GetByIdMealResponse>();

            Assert.Equal(5, model.Meal.Id);
            Assert.Equal("Dorucak", model.Meal.Name);
        }

        [Fact]
        public async Task ReturnsNotFoundGivenInvalidId()
        {
            var adminToken = ApiTokenHelper.GetAdminUserToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
            var response = await Client.GetAsync("api/Meal/0");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}