using PublicApi.Util;
using System;

namespace PublicApi.Endpoints.DietPlanPeriodEndpoints
{
    public class ListPagedDietPlanPeriodRequest : BaseRequest
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public DateTime? StartDateGTE { get; set; }

        public DateTime? StartDateLTE { get; set; }

        public DateTime? EndDateGTE { get; set; }

        public DateTime? EndDateLTE { get; set; }
    }
}

