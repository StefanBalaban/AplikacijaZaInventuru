using PublicApi.Util;
using System;

namespace PublicApi.Endpoints.DietPlanPeriodEndpoints
{
    public class CreateDietPlanPeriodResponse : BaseResponse
    {
        public CreateDietPlanPeriodResponse(Guid correlationId) : base(correlationId)
        {
        }

        public CreateDietPlanPeriodResponse()
        {
        }

        public DietPlanPeriodDto DietPlanPeriod { get; set; }
    }
}

