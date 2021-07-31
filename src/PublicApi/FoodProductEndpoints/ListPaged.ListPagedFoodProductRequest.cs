namespace PublicApi.Util.FoodProductEndpoints
{
    public class ListPagedFoodProductRequest : BaseRequest
    { // TODO nullable
        public int PageIndex { get; set; }

        public int PageSize { get; set; }
        public int? UnitOfMeasureId { get; set; }

        public float? CaloriesGTE { get; set; }

        public float? CaloriesLTE { get; set; }

        public float? Protein { get; set; }
    }

}