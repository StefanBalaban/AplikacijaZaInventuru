using PublicApi.Util;

namespace PublicApi.Endpoints.MealEndpoints
{
    public class ListPagedMealRequest : BaseRequest
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}