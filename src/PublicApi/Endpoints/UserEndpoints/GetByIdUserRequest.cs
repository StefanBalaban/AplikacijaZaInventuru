using PublicApi.Util;

namespace PublicApi.Endpoints.UserEndpoints
{
    public class GetByIdUserRequest : BaseRequest
    {
        public int Id { get; set; }
    }
}

