using PublicApi.Util;

namespace PublicApi.Endpoints.FoodProductEndpoints
{
    public class GetByIdFoodProductRequest : BaseRequest
    {
        public int Id { get; set; }
    }
}