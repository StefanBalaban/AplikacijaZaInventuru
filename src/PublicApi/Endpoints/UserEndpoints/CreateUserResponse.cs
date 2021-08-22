using PublicApi.Util;
using System;

namespace PublicApi.Endpoints.UserEndpoints
{
    public class CreateUserResponse : BaseResponse
    {
        public CreateUserResponse(Guid correlationId) : base(correlationId)
        {
        }

        public CreateUserResponse()
        {
        }

        public UserDto User { get; set; }
    }
}

