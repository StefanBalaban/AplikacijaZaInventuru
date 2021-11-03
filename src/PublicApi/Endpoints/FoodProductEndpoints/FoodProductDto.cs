namespace PublicApi.Endpoints.FoodProductEndpoints
{
    public class FoodProductDto
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; }

        public int UnitOfMeasureId { get; set; }

        public float Calories { get; set; }

        public float Protein { get; set; }

        public float Carbohydrates { get; set; }

        public float Fats { get; set; }
    }
}