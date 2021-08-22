using PublicApi.Util;
using System;

namespace PublicApi.Endpoints.UserWeightEvidentionEndpoints
{
    public class UpdateUserWeightEvidentionRequest : BaseRequest
    {
        public int Id { get; set; }

        public DateTime EvidentationDate { get; set; }

        public float Weight { get; set; }
    }
}
