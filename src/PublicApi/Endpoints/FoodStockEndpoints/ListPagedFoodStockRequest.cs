using PublicApi.Util;

namespace PublicApi.Endpoints.FoodStockEndpoints
{
    public class ListPagedFoodStockRequest : BaseRequest
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int? FoodProductId { get; set; }
    }
}