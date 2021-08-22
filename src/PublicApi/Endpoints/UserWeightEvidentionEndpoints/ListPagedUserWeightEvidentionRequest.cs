using PublicApi.Util;

namespace PublicApi.Endpoints.UserWeightEvidentionEndpoints
{
    public class ListPagedUserWeightEvidentionRequest : BaseRequest
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
