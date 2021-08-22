using PublicApi.Util;
using System;

namespace PublicApi.Endpoints.UserEndpoints
{
    public class GetByIdUserResponse : BaseResponse
    {
        public GetByIdUserResponse(Guid correlationId) : base(correlationId)
        {
        }

        public GetByIdUserResponse()
        {
        }

        public UserDto User { get; set; }
    }
}

