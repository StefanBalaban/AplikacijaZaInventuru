using PublicApi.Util;
using System;

namespace PublicApi.Endpoints.UserEndpoints
{
    public class DeleteUserResponse : BaseResponse
    {
        public DeleteUserResponse(Guid correlationId) : base(correlationId)
        {
        }

        public DeleteUserResponse()
        {
        }

        public string Status { get; set; } = "Deleted";
    }
}

