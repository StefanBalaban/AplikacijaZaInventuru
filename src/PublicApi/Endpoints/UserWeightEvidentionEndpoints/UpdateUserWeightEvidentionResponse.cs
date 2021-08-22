using PublicApi.Util;
using System;

namespace PublicApi.Endpoints.UserWeightEvidentionEndpoints
{
    public class UpdateUserWeightEvidentionResponse : BaseResponse
    {
        public UpdateUserWeightEvidentionResponse(Guid correlationId) : base(correlationId)
        {
        }

        public UpdateUserWeightEvidentionResponse()
        {
        }

        public UserWeightEvidentionDto UserWeightEvidention { get; set; }
    }
}
