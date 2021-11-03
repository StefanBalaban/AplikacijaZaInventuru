using ApplicationCore.Entities;
using Ardalis.Specification;

namespace ApplicationCore.Specifications.UserWeightEvidentionSpecs
{
    public class UserWeightEvidentionFilterPaginatedSpecification : Specification<UserWeightEvidention>
    {
        public UserWeightEvidentionFilterPaginatedSpecification(int? userId, int skip, int take)
        {
            Query.Skip(skip);
            Query.Where(i => userId == null || i.UserId == userId);
            Query.Take(take);
        }
    }
}
