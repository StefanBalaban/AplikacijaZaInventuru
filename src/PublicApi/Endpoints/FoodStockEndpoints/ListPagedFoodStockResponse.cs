using System;
using System.Collections.Generic;
using PublicApi.Util;

namespace PublicApi.Endpoints.FoodStockEndpoints
{
    public class ListPagedFoodStockResponse : BaseResponse
    {
        public ListPagedFoodStockResponse(Guid correlationId) : base(correlationId)
        {
        }

        public ListPagedFoodStockResponse()
        {
        }

        public List<FoodStockDto> FoodStocks { get; set; } = new();
        public int PageCount { get; set; }
    }
}