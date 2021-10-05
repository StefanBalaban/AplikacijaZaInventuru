using ApplicationCore.Entities.MealAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications.MealSpecs
{
    public class MealByIdFilterSpecification : Specification<Meal>
    {
        public MealByIdFilterSpecification(int id)
        {
            Query.Where(x => x.Id == id);
            Query.Include(x => x.Meals);
        }
    }
}