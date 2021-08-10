using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ApplicationCore.Extensions;
using PublicApi.Endpoints.FoodProductEndpoints;
using Xunit;

namespace FunctionalTests.ApiTests.FoodProductEndpoints
{
    [Collection("Sequential")]
    public class CreateEndpoint : IClassFixture<ApiTestFixture>
    {
        private readonly int _calories = 1;
        private readonly int _carbohydrates = 3;
        private readonly int _fats = 4;
        private JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };
        private readonly string _name = "Hljeb";
        private readonly int _protein = 2;
        private readonly int _unitOfMeasureId = 1;

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
            var response = await Client.PostAsync("api/foodproduct", jsonContent);

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task ReturnsSuccessGivenValidNewItemAndAdminUserToken()
        {
            var jsonContent = GetValidNewItemJson();
            var adminToken = ApiTokenHelper.GetAdminUserToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
            var response = await Client.PostAsync("api/foodproduct", jsonContent);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = stringResponse.FromJson<CreateFoodProductResponse>();

            Assert.Equal(_name, model.FoodProduct.Name);
            Assert.Equal(_unitOfMeasureId, model.FoodProduct.UnitOfMeasureId);
            Assert.Equal(_calories, model.FoodProduct.Calories);
            Assert.Equal(_protein, model.FoodProduct.Protein);
            Assert.Equal(_carbohydrates, model.FoodProduct.Carbohydrates);
            Assert.Equal(_fats, model.FoodProduct.Fats);
        }

        private StringContent GetValidNewItemJson()
        {
            var request = new CreateFoodProductRequest
            {
                Name = _name,
                UnitOfMeasureId = _unitOfMeasureId,
                Calories = _calories,
                Protein = _protein,
                Carbohydrates = _carbohydrates,
                Fats = _fats
            };
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            return jsonContent;
        }
    }
}