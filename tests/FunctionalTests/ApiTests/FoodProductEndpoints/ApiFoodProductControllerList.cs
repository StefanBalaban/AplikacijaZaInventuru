using FunctionalTests;
using System.Linq;
using System.Net.Http;
using PublicApi.Util.FoodProductEndpoints;
using ApplicationCore.Extensions;
using System.Threading.Tasks;
using Xunit;
using System.Collections.Generic;

namespace FunctionalTests.ApiTests.FoodProductEndpoints
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
            var response = await Client.GetAsync("/api/foodproduct?pageSize=10");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = stringResponse.FromJson<ListPagedFoodProductResponse>();

            Assert.Equal(10, model.FoodProducts.Count());
        }

        [Fact]
        public async Task ReturnsLast2CatalogItemsGivenPageIndex1()
        {
            var response = await Client.GetAsync("/api/foodproduct?pageSize=10&pageIndex=1");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = stringResponse.FromJson<ListPagedFoodProductResponse>();

            Assert.Equal(2, model.FoodProducts.Count());
        }
    }
}
