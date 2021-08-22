using ApplicationCore.Entities;
using Ardalis.Specification;
using System;

namespace ApplicationCore.Specifications.UserSubscriptionSpecs
{
    public class UserSubscriptionFilterSpecification : Specification<UserSubscription>
    {
        public UserSubscriptionFilterSpecification(DateTime? begginDateGTE, DateTime? begginDateLTE, DateTime? endDateGTE, DateTime? endDateLTE)
        {
            Query.Where(i => !begginDateGTE.HasValue || i.BegginDate >= begginDateGTE);
            Query.Where(i => !begginDateLTE.HasValue || i.BegginDate <= begginDateLTE);
            Query.Where(i => !endDateGTE.HasValue || i.EndDate >= endDateGTE);
            Query.Where(i => !endDateLTE.HasValue || i.EndDate <= endDateLTE);
        }
    }
}
