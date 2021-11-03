using ApplicationCore.Entities.DietPlanAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications.DietPlanSpecs
{
    public class DietPlanFilterPaginatedSpecification : Specification<DietPlan>
    {
        public DietPlanFilterPaginatedSpecification(int? userId, int skip, int take, string name)
        {
            Query.Where(i => name == null || i.Name == name);
            Query.Skip(skip);
            Query.Where(i => userId == null || i.UserId == userId);
            Query.Take(take);
            Query.Include(x => x.DietPlanMeals);
        }
    }

}
