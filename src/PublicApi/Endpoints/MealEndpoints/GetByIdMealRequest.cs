using PublicApi.Util;

namespace PublicApi.Endpoints.MealEndpoints
{
    public class GetByIdMealRequest : BaseRequest
    {
        public int Id { get; set; }
    }
}