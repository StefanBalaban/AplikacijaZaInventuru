using PublicApi.Util;
using System;

namespace PublicApi.Endpoints.DietPlanPeriodEndpoints
{
    public class UpdateDietPlanPeriodResponse : BaseResponse
    {
        public UpdateDietPlanPeriodResponse(Guid correlationId) : base(correlationId)
        {
        }

        public UpdateDietPlanPeriodResponse()
        {
        }

        public DietPlanPeriodDto DietPlanPeriod { get; set; }
    }
}

