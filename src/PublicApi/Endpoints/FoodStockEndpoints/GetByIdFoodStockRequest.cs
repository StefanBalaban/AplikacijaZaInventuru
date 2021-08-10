using PublicApi.Util;

namespace PublicApi.Endpoints.FoodStockEndpoints
{
    public class GetByIdFoodStockRequest : BaseRequest
    {
        public int Id { get; set; }
    }
}