using PublicApi.Util;
using System;

namespace PublicApi.Endpoints.UserEndpoints
{
    public class UpdateUserResponse : BaseResponse
    {
        public UpdateUserResponse(Guid correlationId) : base(correlationId)
        {
        }

        public UpdateUserResponse()
        {
        }

        public UserDto User { get; set; }
    }
}

