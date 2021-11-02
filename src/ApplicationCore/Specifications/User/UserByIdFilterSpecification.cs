using ApplicationCore.Entities.DietPlanAggregate;
using ApplicationCore.Entities.MealAggregate;
using ApplicationCore.Entities.UserAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications.UserSpecs
{
    public class UserByIdFilterSpecification : Specification<User>
    {
        public UserByIdFilterSpecification(int id)
        {
            Query.Where(x => x.Id == id);
            Query.Include(x => x.UserContactInfos);
        }
    }
}