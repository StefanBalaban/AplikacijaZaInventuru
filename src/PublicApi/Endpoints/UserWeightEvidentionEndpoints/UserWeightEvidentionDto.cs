using System;

namespace PublicApi.Endpoints.UserWeightEvidentionEndpoints
{
    public class UserWeightEvidentionDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime EvidentationDate { get; set; }

        public float Weight { get; set; }
    }
}
