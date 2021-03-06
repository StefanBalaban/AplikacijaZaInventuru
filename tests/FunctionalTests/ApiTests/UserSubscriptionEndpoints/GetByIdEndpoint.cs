using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using ApplicationCore.Extensions;
using PublicApi.Endpoints.UserSubscriptionEndpoints;
using Xunit;

namespace FunctionalTests.ApiTests.UserSubscriptionEndpoints
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
            var response = await Client.GetAsync("api/UserSubscription/5");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = stringResponse.FromJson<GetByIdUserSubscriptionResponse>();

            Assert.Equal(5, model.UserSubscription.Id);
            Assert.Equal(new DateTime(2001, 1, 1), model.UserSubscription.BegginDate);           
            Assert.Equal(new DateTime(2001, 2, 1), model.UserSubscription.EndDate);           
            Assert.Equal(new DateTime(2001, 1, 1), model.UserSubscription.PaymentDate);
        }

        [Fact]
        public async Task ReturnsNotFoundGivenInvalidId()
        {
            var adminToken = ApiTokenHelper.GetAdminUserToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
            var response = await Client.GetAsync("api/UserSubscription/0");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}