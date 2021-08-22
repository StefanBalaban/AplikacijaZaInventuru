using PublicApi.Util;
using System;

namespace PublicApi.Endpoints.DietPlanPeriodEndpoints
{
    public class DeleteDietPlanPeriodResponse : BaseResponse
    {
        public DeleteDietPlanPeriodResponse(Guid correlationId) : base(correlationId)
        {
        }

        public DeleteDietPlanPeriodResponse()
        {
        }

        public string Status { get; set; } = "Deleted";
    }
}

