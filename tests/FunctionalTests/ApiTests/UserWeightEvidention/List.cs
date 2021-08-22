using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ApplicationCore.Extensions;
using PublicApi.Endpoints.UserWeightEvidentionEndpoints;
using Xunit;

namespace FunctionalTests.ApiTests.UserWeightEvidentionWeightEvidentationEndpoints
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
            var response = await Client.GetAsync("/api/UserWeightEvidention?pageSize=10");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = stringResponse.FromJson<ListPagedUserWeightEvidentionResponse>();

            Assert.Equal(10, model.UserWeightEvidentions.Count());
        }

        [Fact]
        public async Task ReturnsLast2CatalogItemsGivenPageIndex1()
        {
            var adminToken = ApiTokenHelper.GetAdminUserToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
            var response = await Client.GetAsync("/api/UserWeightEvidention?pageSize=10&pageIndex=1");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = stringResponse.FromJson<ListPagedUserWeightEvidentionResponse>();

            Assert.Equal(2, model.UserWeightEvidentions.Count());
        }
    }
}