using ApplicationCore.Entities.DietPlanAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications.DietPlanSpecs
{
    public class DietPlanFilterPaginatedSpecification : Specification<DietPlan>
    {
        public DietPlanFilterPaginatedSpecification(int skip, int take, string name)
        {
            Query.Where(i => name == null || i.Name == name);
            Query.Skip(skip);
            Query.Take(take);
            Query.Include(x => x.DietPlanMeals);
        }
    }

}
