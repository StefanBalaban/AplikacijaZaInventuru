using PublicApi.Util;
using System;
using System.Collections.Generic;

namespace PublicApi.Endpoints.UserWeightEvidentionEndpoints
{
    public class ListPagedUserWeightEvidentionResponse : BaseResponse
    {
        public ListPagedUserWeightEvidentionResponse(Guid correlationId) : base(correlationId)
        {
        }

        public ListPagedUserWeightEvidentionResponse()
        {
        }

        public List<UserWeightEvidentionDto> UserWeightEvidentions { get; set; } = new List<UserWeightEvidentionDto>();
        public int PageCount { get; set; }
    }
}
