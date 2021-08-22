using PublicApi.Util;
using System;
using System.Collections.Generic;

namespace PublicApi.Endpoints.DietPlanPeriodEndpoints
{
    public class ListPagedDietPlanPeriodResponse : BaseResponse
    {
        public ListPagedDietPlanPeriodResponse(Guid correlationId) : base(correlationId)
        {
        }

        public ListPagedDietPlanPeriodResponse()
        {
        }

        public List<DietPlanPeriodDto> DietPlanPeriods { get; set; } = new List<DietPlanPeriodDto>();
        public int PageCount { get; set; }
    }
}

