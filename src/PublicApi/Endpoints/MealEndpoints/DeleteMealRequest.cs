using PublicApi.Util;

namespace PublicApi.Endpoints.MealEndpoints
{
    public class DeleteMealRequest : BaseRequest
    {
        public int Id { get; set; }
    }
}