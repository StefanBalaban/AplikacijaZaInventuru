using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ApplicationCore.Entities.NotificationAggregate;
using ApplicationCore.Extensions;
using PublicApi.Endpoints.NotificationRuleEndpoints;
using Xunit;

namespace FunctionalTests.ApiTests.NotificationRuleEndpoints
{
    [Collection("Sequential")]
    public class CreateEndpoint : IClassFixture<ApiTestFixture>
    {
        private readonly int _foodProductId = 1;
        private readonly int _contactInfoId = 1;
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
            var response = await Client.PostAsync("api/NotificationRule", jsonContent);

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task ReturnsSuccessGivenValidNewItemAndAdminUserToken()
        {
            var jsonContent = GetValidNewItemJson();
            var adminToken = ApiTokenHelper.GetAdminUserToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
            var response = await Client.PostAsync("api/NotificationRule", jsonContent);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = stringResponse.FromJson<CreateNotificationRuleResponse>();

            Assert.Equal(_foodProductId, model.NotificationRule.FoodProductId);
            Assert.Equal(_contactInfoId, model.NotificationRule.NotificationRuleUserContactInfos[0].UserContactInfosId);
        }

        private StringContent GetValidNewItemJson()
        {
            var request = new CreateNotificationRuleRequest
            {
                FoodProductId = _foodProductId,
                NotificationRuleUserContactInfos = new List<NotificationRuleUserContactInfos>
                {
                    new NotificationRuleUserContactInfos { UserContactInfosId = _contactInfoId }
                }
            };
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            return jsonContent;
        }
    }
}