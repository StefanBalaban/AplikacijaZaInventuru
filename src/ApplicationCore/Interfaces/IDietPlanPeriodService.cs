using ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IDietPlanPeriodService : ICrudServices<DietPlanPeriod>
    {
        Task<List<DietPlanPeriod>> ListAll();
    }
}

