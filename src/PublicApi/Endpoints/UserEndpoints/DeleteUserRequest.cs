using PublicApi.Util;

namespace PublicApi.Endpoints.UserEndpoints
{
    public class DeleteUserRequest : BaseRequest
    {
        public int Id { get; set; }
    }
}

