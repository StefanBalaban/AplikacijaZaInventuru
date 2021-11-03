using PublicApi.Util;
using System;

namespace PublicApi.Endpoints.DietPlanPeriodEndpoints
{
    public class CreateDietPlanPeriodRequest : BaseRequest 
    {
        public int UserId { get; set; }
        public int DietPlanId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}

