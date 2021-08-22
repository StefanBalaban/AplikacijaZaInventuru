using PublicApi.Util;
using System;

namespace PublicApi.Endpoints.UserWeightEvidentionEndpoints
{
    public class DeleteUserWeightEvidentionResponse : BaseResponse
    {
        public DeleteUserWeightEvidentionResponse(Guid correlationId) : base(correlationId)
        {
        }

        public DeleteUserWeightEvidentionResponse()
        {
        }

        public string Status { get; set; } = "Deleted";
    }
}
