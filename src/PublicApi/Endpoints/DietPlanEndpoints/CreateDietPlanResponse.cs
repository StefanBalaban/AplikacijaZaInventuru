using PublicApi.Util;
using System;

namespace PublicApi.Endpoints.DietPlanEndpoints
{
    public class CreateDietPlanResponse : BaseResponse
    {
        public CreateDietPlanResponse(Guid correlationId) : base(correlationId)
        {
        }

        public CreateDietPlanResponse()
        {
        }

        public DietPlanDto DietPlan { get; set; }
    }
}
