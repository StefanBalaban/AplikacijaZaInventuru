using ApplicationCore.Entities;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class DietPlanPeriodService : IDietPlanPeriodService
    {
        private readonly IAsyncRepository<DietPlanPeriod> _dietPlanPeriodRepository;
        private readonly IAppLogger<DietPlanPeriodService> _logger;
        public DietPlanPeriodService(IAsyncRepository<DietPlanPeriod> dietPlanPeriodRepository, IAppLogger<DietPlanPeriodService> logger)
        {
            _dietPlanPeriodRepository = dietPlanPeriodRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<DietPlanPeriod>> GetAsync()
        {
            return await _dietPlanPeriodRepository.ListAllAsync();
        }

        public async Task<DietPlanPeriod> GetAsync(int id)
        {
            var dietPlanPeriod = await _dietPlanPeriodRepository.GetByIdAsync(id);
            Guard.Against.EntityNotFound(dietPlanPeriod, nameof(DietPlanPeriod));
            return dietPlanPeriod;
        }

        public async Task<ListEntity<DietPlanPeriod>> GetAsync(Specification<DietPlanPeriod> filterSpec, Specification<DietPlanPeriod> pagedSpec)
        {
            return new ListEntity<DietPlanPeriod> { List = await _dietPlanPeriodRepository.ListAsync(pagedSpec), Count = await _dietPlanPeriodRepository.CountAsync(filterSpec) };
        }

        public async Task<DietPlanPeriod> PostAsync(DietPlanPeriod t)
        {
            Guard.Against.ModelStateIsInvalid(t, nameof(DietPlanPeriod));
            return await _dietPlanPeriodRepository.AddAsync(new DietPlanPeriod { DietPlanId = t.DietPlanId, StartDate = t.StartDate, EndDate = t.EndDate, UserId = t.UserId });
        }

        public async Task<DietPlanPeriod> PutAsync(DietPlanPeriod t)
        {
            Guard.Against.ModelStateIsInvalid(t, nameof(DietPlanPeriod));
            var dietPlanPeriod = await _dietPlanPeriodRepository.GetByIdAsync(t.Id);
            Guard.Against.EntityNotFound(dietPlanPeriod, nameof(DietPlanPeriod));
            dietPlanPeriod.DietPlanId = t.DietPlanId;
            dietPlanPeriod.StartDate = t.StartDate;
            dietPlanPeriod.EndDate = t.EndDate;
            await _dietPlanPeriodRepository.UpdateAsync(dietPlanPeriod);
            return dietPlanPeriod;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var dietPlanPeriod = await _dietPlanPeriodRepository.GetByIdAsync(id);
            Guard.Against.EntityNotFound(dietPlanPeriod, nameof(DietPlanPeriod));
            await _dietPlanPeriodRepository.DeleteAsync(dietPlanPeriod);
            return true;
        }
    }
}


