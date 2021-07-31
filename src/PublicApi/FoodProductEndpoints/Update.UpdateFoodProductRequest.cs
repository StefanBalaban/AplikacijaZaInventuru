namespace PublicApi.Util.FoodProductEndpoints
{
    public class UpdateFoodProductRequest : BaseRequest
    {
        public string Name { get; set; }

        public int UnitOfMeasureId { get; set; }

        public float Calories { get; set; }

        public float Protein { get; set; }

        public float Carbohydrates { get; set; }
    }
}