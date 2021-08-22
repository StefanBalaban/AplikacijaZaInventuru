using ApplicationCore.Extensions;
using PublicApi.Endpoints.DietPlanPeriodEndpoints;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace FunctionalTests.ApiTests.DietPlanPeriodEndpoints
{
    [Collection("Sequential")]
    public class CreateEndpoint : IClassFixture<ApiTestFixture>
    {
        private readonly int _dietPlanId = 1;
        private readonly DateTime _startDate = DateTime.Today.AddDays(-1);
        private readonly DateTime _endDate = DateTime.Today.AddDays(1);
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
            var response = await Client.PostAsync("api/DietPlanPeriod", jsonContent);

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task ReturnsSuccessGivenValidNewItemAndAdminUserToken()
        {
            var jsonContent = GetValidNewItemJson();
            var adminToken = ApiTokenHelper.GetAdminUserToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
            var response = await Client.PostAsync("api/DietPlanPeriod", jsonContent);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = stringResponse.FromJson<CreateDietPlanPeriodResponse>();

            Assert.Equal(_dietPlanId, model.DietPlanPeriod.DietPlanId);
            Assert.Equal(_startDate, model.DietPlanPeriod.StartDate);
            Assert.Equal(_endDate, model.DietPlanPeriod.EndDate);
        }

        private StringContent GetValidNewItemJson()
        {
            var request = new CreateDietPlanPeriodRequest
            {
                DietPlanId = _dietPlanId,
                StartDate = _startDate,
                EndDate = _endDate
            };
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            return jsonContent;
        }
    }
}