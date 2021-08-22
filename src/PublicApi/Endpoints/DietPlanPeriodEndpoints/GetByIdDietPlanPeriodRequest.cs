using PublicApi.Util;

namespace PublicApi.Endpoints.DietPlanPeriodEndpoints
{
    public class GetByIdDietPlanPeriodRequest : BaseRequest
    {
        public int Id { get; set; }
    }
}

