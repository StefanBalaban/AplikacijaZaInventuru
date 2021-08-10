using System;
using PublicApi.Util;

namespace PublicApi.Endpoints.MealEndpoints
{
    public class DeleteMealResponse : BaseResponse
    {
        public DeleteMealResponse(Guid correlationId) : base(correlationId)
        {
        }

        public DeleteMealResponse()
        {
        }

        public string Status { get; set; } = "Deleted";
    }
}