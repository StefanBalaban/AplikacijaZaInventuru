using ApplicationCore.Entities.NotificationAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications.NotificationRuleSpecs
{
    public class NotificationRuleFilterPaginatedSpecification : Specification<NotificationRule>
    {
        public NotificationRuleFilterPaginatedSpecification(int? userId, int skip, int take, int? foodProductId)
        {
            Query.Where(i => !foodProductId.HasValue || i.FoodProductId == foodProductId);
            Query.Skip(skip);
            Query.Where(i => userId == null || i.UserId == userId);
            Query.Take(take);
            Query.Include(x => x.NotificationRuleUserContactInfos).ThenInclude(x =>x.UserContactInfo);
        }
    }
}

