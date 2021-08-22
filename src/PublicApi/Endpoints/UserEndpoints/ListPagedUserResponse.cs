using PublicApi.Util;
using System;
using System.Collections.Generic;

namespace PublicApi.Endpoints.UserEndpoints
{
    public class ListPagedUserResponse : BaseResponse
    {
        public ListPagedUserResponse(Guid correlationId) : base(correlationId)
        {
        }

        public ListPagedUserResponse()
        {
        }

        public List<UserDto> Users { get; set; } = new List<UserDto>();
        public int PageCount { get; set; }
    }
}

