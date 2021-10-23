using ApplicationCore.Entities.DietPlanAggregate;
using ApplicationCore.Entities.MealAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications.DietPlanSpecs
{
    public class DietPlanByIdFilterSpecification : Specification<DietPlan>
    {
        public DietPlanByIdFilterSpecification(int id)
        {
            Query.Where(x => x.Id == id);
            Query.Include(x => x.DietPlanMeals);
        }
    }
}