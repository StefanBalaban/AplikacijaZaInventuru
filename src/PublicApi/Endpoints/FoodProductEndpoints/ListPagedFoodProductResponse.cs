using System;
using System.Collections.Generic;
using PublicApi.Util;

namespace PublicApi.Endpoints.FoodProductEndpoints
{
    public class ListPagedFoodProductResponse : BaseResponse
    {
        public ListPagedFoodProductResponse(Guid correlationId) : base(correlationId)
        {
        }

        public ListPagedFoodProductResponse()
        {
        }

        public List<FoodProductDto> FoodProducts { get; set; } = new();
        public int PageCount { get; set; }
    }
}