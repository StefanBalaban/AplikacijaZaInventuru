using System;
using System.Collections.Generic;
using PublicApi.Util;

namespace PublicApi.Endpoints.MealEndpoints
{
    public class ListPagedMealResponse : BaseResponse
    {
        public ListPagedMealResponse(Guid correlationId) : base(correlationId)
        {
        }

        public ListPagedMealResponse()
        {
        }

        public List<MealDto> Meals { get; set; } = new();
        public int PageCount { get; set; }
    }
}