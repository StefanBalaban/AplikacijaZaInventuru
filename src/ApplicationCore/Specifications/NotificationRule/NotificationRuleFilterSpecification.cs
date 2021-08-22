using ApplicationCore.Entities.NotificationAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications.NotificationRuleSpecs
{
    public class NotificationRuleFilterSpecification : Specification<NotificationRule>
    {
        public NotificationRuleFilterSpecification(int? foodProductId)
        {
            Query.Where(i => !foodProductId.HasValue || i.FoodProductId == foodProductId);
        }
    }
}

