using ApplicationCore.Entities;
using Ardalis.Specification;
using System;

namespace ApplicationCore.Specifications.DietPlanPeriodSpecs
{
    public class DietPlanPeriodFilterPaginatedSpecification : Specification<DietPlanPeriod>
    {
        public DietPlanPeriodFilterPaginatedSpecification(int? userId, int skip, int take, DateTime? startDateGTE, DateTime? startDateLTE, DateTime? endDateGTE, DateTime? endDateLTE)
        {
            Query.Where(i => !startDateGTE.HasValue || i.StartDate >= startDateGTE);
            Query.Where(i => !startDateLTE.HasValue || i.StartDate <= startDateLTE);
            Query.Where(i => !endDateGTE.HasValue || i.EndDate >= endDateGTE);
            Query.Where(i => !endDateLTE.HasValue || i.EndDate <= endDateLTE);
            Query.Skip(skip);
            Query.Where(i => userId == null || i.UserId == userId);
            Query.Take(take);
        }
    }
}


