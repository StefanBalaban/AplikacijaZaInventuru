using ApplicationCore.Entities.DietPlanAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications.DietPlanSpecs
{
    public class DietPlanMealFilterSpecification : Specification<DietPlanMeal>
    {
        public DietPlanMealFilterSpecification(int dietPlanId)
        {
            Query.Where(x => x.DietPlanId == dietPlanId);
        }
    }
}