using PublicApi.Util;
using System;

namespace PublicApi.Endpoints.UserWeightEvidentionEndpoints
{
    public class CreateUserWeightEvidentionResponse : BaseResponse
    {
        public CreateUserWeightEvidentionResponse(Guid correlationId) : base(correlationId)
        {
        }

        public CreateUserWeightEvidentionResponse()
        {
        }

        public UserWeightEvidentionDto UserWeightEvidention { get; set; }
    }
}
