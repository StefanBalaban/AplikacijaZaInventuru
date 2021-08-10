using System;

namespace ApplicationCore.Entities.DietPlanAggregate
{
    public class DietPlanPeriod : BaseEntity
    {
        public DietPlan DietPlan { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}