using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ApplicationCore.Extensions;
using PublicApi.Endpoints.FoodStockEndpoints;
using Xunit;

namespace FunctionalTests.ApiTests.FoodStockEndpoints
{
    [Collection("Sequential")]
    public class CreateEndpoint : IClassFixture<ApiTestFixture>
    {
        private readonly float _amount = 4;
        private readonly DateTime _bestUseByDate = new DateTime().AddDays(3);
        private readonly DateTime _dateOfPurchase = new();
        private readonly int _foodProductId = 1;
        private JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };
        private readonly float _lowerAmount = 5;
        private readonly float _upperAmount = 5;

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
            var response = await Client.PostAsync("api/FoodStock", jsonContent);

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task ReturnsSuccessGivenValidNewItemAndAdminUserToken()
        {
            var jsonContent = GetValidNewItemJson();
            var adminToken = ApiTokenHelper.GetAdminUserToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
            var response = await Client.PostAsync("api/FoodStock", jsonContent);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = stringResponse.FromJson<CreateFoodStockResponse>();

            Assert.Equal(_foodProductId, model.FoodStock.FoodProductId);
            Assert.Equal(_amount, model.FoodStock.Amount);
            Assert.Equal(_upperAmount, model.FoodStock.UpperAmount);
            Assert.Equal(_lowerAmount, model.FoodStock.LowerAmount);
            Assert.Equal(_dateOfPurchase, model.FoodStock.DateOfPurchase);
            Assert.Equal(_bestUseByDate, model.FoodStock.BestUseByDate);
        }

        private StringContent GetValidNewItemJson()
        {
            var request = new CreateFoodStockRequest
            {
                FoodProductId = _foodProductId,
                Amount = _amount,
                UpperAmount = _upperAmount,
                LowerAmount = _lowerAmount,
                DateOfPurchase = _dateOfPurchase,
                BestUseByDate = _bestUseByDate
            };
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            return jsonContent;
        }
    }
}