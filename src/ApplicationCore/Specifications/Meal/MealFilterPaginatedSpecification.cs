using ApplicationCore.Entities.MealAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications.MealSpecs
{
    public class MealFilterPaginatedSpecification : Specification<Meal>
    {
        public MealFilterPaginatedSpecification(int? userId, int skip, int take)
        {
            Query.Skip(skip);
            Query.Where(i => userId == null || i.UserId == userId);
            Query.Take(take);
            Query.Include(x => x.Meals);
        }
    }
}