using ApplicationCore.Entities.MealAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications.MealSpecs
{
    public class MealFilterPaginatedSpecification : Specification<Meal>
    {
        public MealFilterPaginatedSpecification(int skip, int take)
        {
            Query.Skip(skip);
            Query.Take(take);
        }
    }
}