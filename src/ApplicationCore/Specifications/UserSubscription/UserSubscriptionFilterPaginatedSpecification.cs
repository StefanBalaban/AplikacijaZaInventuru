using ApplicationCore.Entities;
using Ardalis.Specification;
using System;

namespace ApplicationCore.Specifications.UserSubscriptionSpecs
{
    public class UserSubscriptionFilterPaginatedSpecification : Specification<UserSubscription>
    {
        public UserSubscriptionFilterPaginatedSpecification(int skip, int take, DateTime? begginDateGTE, DateTime? begginDateLTE, DateTime? endDateGTE, DateTime? endDateLTE)
        {
            Query.Where(i => !begginDateGTE.HasValue || i.BegginDate >= begginDateGTE);
            Query.Where(i => !begginDateLTE.HasValue || i.BegginDate <= begginDateLTE);
            Query.Where(i => !endDateGTE.HasValue || i.EndDate >= endDateGTE);
            Query.Where(i => !endDateLTE.HasValue || i.EndDate <= endDateLTE);
            Query.Skip(skip);
            Query.Take(take);
        }
    }
}
