using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ApplicationCore.Entities.MealAggregate;
using ApplicationCore.Extensions;
using PublicApi.Endpoints.MealEndpoints;
using Xunit;

namespace FunctionalTests.ApiTests.MealEndpoints
{
    [Collection("Sequential")]
    public class CreateEndpoint : IClassFixture<ApiTestFixture>
    {
        private readonly string _name = "Dorucak";
        private readonly int _foodProductId = 1;
        private readonly float _amount = 1;
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
            var response = await Client.PostAsync("api/Meal", jsonContent);

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task ReturnsSuccessGivenValidNewItemAndAdminUserToken()
        {
            var jsonContent = GetValidNewItemJson();
            var adminToken = ApiTokenHelper.GetAdminUserToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
            var response = await Client.PostAsync("api/Meal", jsonContent);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = stringResponse.FromJson<CreateMealResponse>();

            Assert.Equal(_name, model.Meal.Name);
        }

        private StringContent GetValidNewItemJson()
        {
            var request = new CreateMealRequest
            {
                Name = _name,
                Meals = new List<MealItem>() { new MealItem() { Amount = _amount, FoodProductId = _foodProductId } }
             
            };
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            return jsonContent;
        }
    }
}