using ApplicationCore.Entities.DietPlanAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications.DietPlanSpecs
{
    public class DietPlanFilterSpecification : Specification<DietPlan>
    {
        public DietPlanFilterSpecification(string name)
        {
            Query.Where(i => name == null || i.Name == name);
        }
    }

}
