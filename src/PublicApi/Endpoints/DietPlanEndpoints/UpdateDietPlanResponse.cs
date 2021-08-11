using PublicApi.Util;
using System;

namespace PublicApi.Endpoints.DietPlanEndpoints
{
    public class UpdateDietPlanResponse : BaseResponse
    {
        public UpdateDietPlanResponse(Guid correlationId) : base(correlationId)
        {
        }

        public UpdateDietPlanResponse()
        {
        }

        public DietPlanDto DietPlan { get; set; }
    }
}
