using PublicApi.Util;
using System;

namespace PublicApi.Endpoints.DietPlanPeriodEndpoints
{
    public class UpdateDietPlanPeriodRequest : BaseRequest
    {
        public int Id { get; set; }

        public int DietPlanId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}

