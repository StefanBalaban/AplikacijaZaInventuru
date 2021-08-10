using PublicApi.Util;

namespace PublicApi.Endpoints.FoodStockEndpoints
{
    public class DeleteFoodStockRequest : BaseRequest
    {
        public int Id { get; set; }
    }
}