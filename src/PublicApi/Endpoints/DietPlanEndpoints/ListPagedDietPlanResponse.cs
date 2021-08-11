using PublicApi.Util;
using System;
using System.Collections.Generic;

namespace PublicApi.Endpoints.DietPlanEndpoints
{
    public class ListPagedDietPlanResponse : BaseResponse
    {
        public ListPagedDietPlanResponse(Guid correlationId) : base(correlationId)
        {
        }

        public ListPagedDietPlanResponse()
        {
        }

        public List<DietPlanDto> DietPlans { get; set; } = new List<DietPlanDto>();
        public int PageCount { get; set; }
    }
}
