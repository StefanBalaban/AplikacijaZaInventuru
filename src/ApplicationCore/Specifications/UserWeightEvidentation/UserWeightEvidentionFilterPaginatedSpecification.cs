using ApplicationCore.Entities;
using Ardalis.Specification;

namespace ApplicationCore.Specifications.UserWeightEvidentionSpecs
{
    public class UserWeightEvidentionFilterPaginatedSpecification : Specification<UserWeightEvidention>
    {
        public UserWeightEvidentionFilterPaginatedSpecification(int skip, int take)
        {
            Query.Skip(skip);
            Query.Take(take);
        }
    }
}
