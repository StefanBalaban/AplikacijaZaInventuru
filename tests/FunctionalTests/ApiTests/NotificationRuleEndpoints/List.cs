using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ApplicationCore.Extensions;
using PublicApi.Endpoints.NotificationRuleEndpoints;
using Xunit;

namespace FunctionalTests.ApiTests.NotificationRuleEndpoints
{
    [Collection("Sequential")]
    public class ApiCatalogControllerList : IClassFixture<ApiTestFixture>
    {
        public ApiCatalogControllerList(ApiTestFixture factory)
        {
            Client = factory.CreateClient();
        }

        public HttpClient Client { get; }

        [Fact]
        public async Task ReturnsFirst10CatalogItems()
        {
            var adminToken = ApiTokenHelper.GetAdminUserToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
            var response = await Client.GetAsync("/api/NotificationRule?pageSize=10");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = stringResponse.FromJson<ListPagedNotificationRuleResponse>();

            Assert.Equal(10, model.NotificationRules.Count());
        }

        [Fact]
        public async Task ReturnsLast2CatalogItemsGivenPageIndex1()
        {
            var adminToken = ApiTokenHelper.GetAdminUserToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
            var response = await Client.GetAsync("/api/NotificationRule?pageSize=10&pageIndex=1");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = stringResponse.FromJson<ListPagedNotificationRuleResponse>();

            Assert.Equal(2, model.NotificationRules.Count());
        }
    }
}