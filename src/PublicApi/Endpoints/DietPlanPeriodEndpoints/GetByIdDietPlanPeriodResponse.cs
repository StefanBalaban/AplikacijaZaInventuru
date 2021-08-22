using PublicApi.Util;
using System;

namespace PublicApi.Endpoints.DietPlanPeriodEndpoints
{
    public class GetByIdDietPlanPeriodResponse : BaseResponse
    {
        public GetByIdDietPlanPeriodResponse(Guid correlationId) : base(correlationId)
        {
        }

        public GetByIdDietPlanPeriodResponse()
        {
        }

        public DietPlanPeriodDto DietPlanPeriod { get; set; }
    }
}

