using PublicApi.Util;

namespace PublicApi.Endpoints.FoodProductEndpoints
{
    public class DeleteFoodProductRequest : BaseRequest
    {
        public int Id { get; set; }
    }
}