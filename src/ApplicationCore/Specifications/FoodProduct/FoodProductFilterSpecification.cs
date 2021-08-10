using ApplicationCore.Entities;
using Ardalis.Specification;

namespace ApplicationCore.Specifications.FoodProductSpecs
{
    public class FoodProductFilterSpecification : Specification<FoodProduct>
    {
        public FoodProductFilterSpecification(int? unitOfMeasureId, float? caloriesGTE, float? caloriesLTE,
            float? protein)
        {
            Query.Where(i => !unitOfMeasureId.HasValue || i.UnitOfMeasureId == unitOfMeasureId);
            Query.Where(i => !caloriesGTE.HasValue || i.Calories >= caloriesGTE);
            Query.Where(i => !caloriesLTE.HasValue || i.Calories <= caloriesLTE);
            Query.Where(i => !protein.HasValue || i.Protein == protein);
        }
    }
}