using PublicApi.Util;

namespace PublicApi.Endpoints.DietPlanEndpoints
{
    public class DeleteDietPlanRequest : BaseRequest
    {
        public int Id { get; set; }
    }
}
