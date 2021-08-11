using PublicApi.Util;

namespace PublicApi.Endpoints.DietPlanEndpoints
{
    public class GetByIdDietPlanRequest : BaseRequest
    {
        public int Id { get; set; }
    }
}
