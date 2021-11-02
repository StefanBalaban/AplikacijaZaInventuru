using ApplicationCore.Entities.UserAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications.UserSpecs
{
    public class UserByNameFilterSpecification : Specification<User>
    {
        public UserByNameFilterSpecification(string name)
        {
            Query.Where(x => x.FirstName == name);
            Query.Include(x => x.UserContactInfos);
        }
    }
}