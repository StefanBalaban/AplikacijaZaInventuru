using PublicApi.Util;

namespace PublicApi.Endpoints.DietPlanEndpoints
{
    public class ListPagedDietPlanRequest : BaseRequest
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string? Name { get; set; }
    }
}
