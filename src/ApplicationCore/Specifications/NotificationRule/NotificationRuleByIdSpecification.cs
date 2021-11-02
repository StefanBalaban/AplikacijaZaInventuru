using ApplicationCore.Entities.DietPlanAggregate;
using ApplicationCore.Entities.NotificationAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications.NotificationRuleSpecs
{
    public class NotificationRuleByIdFilterSpecification : Specification<NotificationRule>
    {
        public NotificationRuleByIdFilterSpecification(int id)
        {
            Query.Where(x => x.Id == id);
            Query.Include(x => x.NotificationRuleUserContactInfos);
        }
    }
}