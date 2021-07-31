using Ardalis.GuardClauses;
using System;

namespace ApplicationCore.Entities.DietPlanAggregate
{
    public class DietPlanPeriod : BaseEntity
    {
        public DietPlan DietPlan { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public DietPlanPeriod()
        {
        }
    }
}