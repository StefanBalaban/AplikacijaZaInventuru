using PublicApi.Util;
using System;

namespace PublicApi.Endpoints.UserWeightEvidentionEndpoints
{
    public class CreateUserWeightEvidentionRequest : BaseRequest
    {
        public int UserId { get; set; }

        public DateTime EvidentationDate { get; set; }

        public float Weight { get; set; }
    }
}
