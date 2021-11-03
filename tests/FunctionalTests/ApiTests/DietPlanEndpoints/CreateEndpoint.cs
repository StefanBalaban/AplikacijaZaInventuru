using ApplicationCore.Entities.DietPlanAggregate;
using ApplicationCore.Extensions;
using PublicApi.Endpoints.DietPlanEndpoints;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace FunctionalTests.ApiTests.DietPlanEndpoints
{
    [Collection("Sequential")]
    public class CreateEndpoint : IClassFixture<ApiTestFixture>
    {
        private readonly string _name = "Sedmica";
        private readonly string _mealName = "Dorucak";
        private readonly int _mealId = 1;
        private readonly int _dietPlanId = 13;
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
            var response = await Client.PostAsync("api/DietPlan", jsonContent);

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task ReturnsSuccessGivenValidNewItemAndAdminUserToken()
        {
            var jsonContent = GetValidNewItemJson();
            var adminToken = ApiTokenHelper.GetAdminUserToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
            var response = await Client.PostAsync("api/DietPlan", jsonContent);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = stringResponse.FromJson<CreateDietPlanResponse>();

            Assert.Equal(_name, model.DietPlan.Name);
            Assert.Equal(_mealId, model.DietPlan.DietPlanMeals[0].MealId);
        }

        private StringContent GetValidNewItemJson()
        {
            var request = new CreateDietPlanRequest
            {
                Name = _name,
                //DietPlanMeals = new List<DietPlanMeal>() { new DietPlanMeal { MealId = _mealId, DietPlanId = 13 } }

            };
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            return jsonContent;
        }
    }
}