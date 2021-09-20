using ApplicationCore.Entities;
using Ardalis.Specification;
using System.Xml.Linq;

namespace ApplicationCore.Specifications.FoodProductSpecs
{
    public class FoodProductFilterPaginatedSpecification : Specification<FoodProduct>
    {
        public FoodProductFilterPaginatedSpecification(int skip, int take, int? unitOfMeasureId, float? caloriesGTE,
            float? caloriesLTE, float? protein, string name)
        {
            Query.Where(i => !unitOfMeasureId.HasValue || i.UnitOfMeasureId == unitOfMeasureId);
            Query.Where(i => !caloriesGTE.HasValue || i.Calories >= caloriesGTE);
            Query.Where(i => !caloriesLTE.HasValue || i.Calories <= caloriesLTE);
            Query.Where(i => !protein.HasValue || i.Protein == protein);
            Query.Skip(skip);
            Query.Take(take);
            Query.Include(x => x.UnitOfMeasure);
Query.Where(i => name == null || i.Name.Contains(name));
}
    }
}