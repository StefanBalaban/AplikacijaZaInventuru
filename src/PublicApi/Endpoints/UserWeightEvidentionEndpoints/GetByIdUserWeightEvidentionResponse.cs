using PublicApi.Util;
using System;

namespace PublicApi.Endpoints.UserWeightEvidentionEndpoints
{
    public class GetByIdUserWeightEvidentionResponse : BaseResponse
    {
        public GetByIdUserWeightEvidentionResponse(Guid correlationId) : base(correlationId)
        {
        }

        public GetByIdUserWeightEvidentionResponse()
        {
        }

        public UserWeightEvidentionDto UserWeightEvidention { get; set; }
    }
}
