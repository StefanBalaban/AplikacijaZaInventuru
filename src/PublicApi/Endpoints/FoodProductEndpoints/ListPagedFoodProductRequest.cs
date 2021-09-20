using PublicApi.Util;

namespace PublicApi.Endpoints.FoodProductEndpoints
{
    public class ListPagedFoodProductRequest : BaseRequest
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }
        public string Name { get; set; }

        public int? UnitOfMeasureId { get; set; }

        public float? CaloriesGTE { get; set; }

        public float? CaloriesLTE { get; set; }

        public float? Protein { get; set; }
    }
}