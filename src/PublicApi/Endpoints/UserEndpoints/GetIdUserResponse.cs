using PublicApi.Util;
using System;

namespace PublicApi.Endpoints.UserEndpoints
{
    public class GetIdUserResponse : BaseResponse
    {
        public GetIdUserResponse(Guid correlationId) : base(correlationId)
        {
        }

        public GetIdUserResponse()
        {
        }

        public UserDto User { get; set; }
    }
}

