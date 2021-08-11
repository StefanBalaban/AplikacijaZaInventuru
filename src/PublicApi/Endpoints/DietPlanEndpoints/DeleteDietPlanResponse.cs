using PublicApi.Util;
using System;

namespace PublicApi.Endpoints.DietPlanEndpoints
{
    public class DeleteDietPlanResponse : BaseResponse
    {
        public DeleteDietPlanResponse(Guid correlationId) : base(correlationId)
        {
        }

        public DeleteDietPlanResponse()
        {
        }

        public string Status { get; set; } = "Deleted";
    }
}
