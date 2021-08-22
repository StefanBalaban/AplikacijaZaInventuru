using PublicApi.Util;

namespace PublicApi.Endpoints.UserEndpoints
{
    public class ListPagedUserRequest : BaseRequest
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}

