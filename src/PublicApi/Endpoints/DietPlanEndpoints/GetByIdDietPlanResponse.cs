using PublicApi.Util;
using System;

namespace PublicApi.Endpoints.DietPlanEndpoints
{
    public class GetByIdDietPlanResponse : BaseResponse
    {
        public GetByIdDietPlanResponse(Guid correlationId) : base(correlationId)
        {
        }

        public GetByIdDietPlanResponse()
        {
        }

        public DietPlanDto DietPlan { get; set; }
    }
}
