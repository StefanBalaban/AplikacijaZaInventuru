using ApplicationCore.Entities.UserAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications.UserSpecs
{
    public class UserFilterPaginatedSpecification : Specification<User>
    {
        public UserFilterPaginatedSpecification(int skip, int take)
        {
            Query.Skip(skip);
            Query.Take(take);
            Query.Include(x => x.UserContactInfos);
        }
    }
}

