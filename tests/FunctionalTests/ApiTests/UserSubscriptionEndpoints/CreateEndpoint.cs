using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PublicApi.Endpoints.UserSubscriptionEndpoints;
using ApplicationCore.Extensions;
using Xunit;

namespace FunctionalTests.ApiTests.UserSubscriptionEndpoints
{
    [Collection("Sequential")]
    public class CreateEndpoint : IClassFixture<ApiTestFixture>
    {
        private readonly DateTime _startDate = new DateTime(2002, 2, 2);
        private readonly DateTime _endDate = new DateTime(2002, 3, 2);
        private readonly DateTime _paymentDate = new DateTime(2002, 1, 2);
        private JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        public CreateEndpoint(ApiTestFixture factory)
        {
            Client = factory.CreateClient();
        }

        public HttpClient Client { get; }

        [Fact]
        public async Task ReturnsNotAuthorizedGivenNormalUserSubscriptionToken()
        {
            var jsonContent = GetValidNewItemJson();
            var token = ApiTokenHelper.GetNormalUserToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await Client.PostAsync("api/UserSubscription", jsonContent);

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task ReturnsSuccessGivenValidNewItemAndAdminUserSubscriptionToken()
        {
            var jsonContent = GetValidNewItemJson();
            var adminToken = ApiTokenHelper.GetAdminUserToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
            var response = await Client.PostAsync("api/UserSubscription", jsonContent);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = stringResponse.FromJson<CreateUserSubscriptionResponse>();

            Assert.Equal(_startDate, model.UserSubscription.BegginDate);
            Assert.Equal(_endDate, model.UserSubscription.EndDate);
            Assert.Equal(_paymentDate, model.UserSubscription.PaymentDate);
        }

        private StringContent GetValidNewItemJson()
        {
            var request = new CreateUserSubscriptionRequest
            {
                BegginDate = _startDate,
                EndDate = _endDate,
                PaymentDate = _paymentDate
             
            };
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            return jsonContent;
        }
    }
}