using ApplicationCore.Entities.DietPlanAggregate;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using Ardalis.Specification;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Extensions;
using ApplicationCore.Specifications.DietPlanSpecs;

namespace ApplicationCore.Services
{
    public class DietPlanService : IDietPlanService
    {
        private readonly IAsyncRepository<DietPlan> _dietPlanRepository;
        private readonly IAppLogger<DietPlanService> _logger;
        public DietPlanService(IAsyncRepository<DietPlan> dietPlanRepository, IAppLogger<DietPlanService> logger)
        {
            _dietPlanRepository = dietPlanRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<DietPlan>> GetAsync()
        {
            return await _dietPlanRepository.ListAllAsync();
        }

        public async Task<DietPlan> GetAsync(int id)
        {
            var spec = new DietPlanByIdFilterSpecification(id);
            var dietPlan = await _dietPlanRepository.FirstOrDefaultAsync(spec);
            Guard.Against.EntityNotFound(dietPlan, nameof(DietPlan));
            return dietPlan;
        }

        public async Task<ListEntity<DietPlan>> GetAsync(Specification<DietPlan> filterSpec, Specification<DietPlan> pagedSpec)
        {
            return new ListEntity<DietPlan> { List = await _dietPlanRepository.ListAsync(pagedSpec), Count = await _dietPlanRepository.CountAsync(filterSpec) };
        }

        public async Task<DietPlan> PostAsync(DietPlan t)
        {
            Guard.Against.ModelStateIsInvalid(t, nameof(DietPlan));
            return await _dietPlanRepository.AddAsync(new DietPlan { DietPlanMeals = t.DietPlanMeals, Name = t.Name });
        }

        public async Task<DietPlan> PutAsync(DietPlan t)
        {
            Guard.Against.ModelStateIsInvalid(t, nameof(DietPlan));
            var spec = new DietPlanByIdFilterSpecification(t.Id);
            var dietPlan = await _dietPlanRepository.FirstOrDefaultAsync(spec);
            Guard.Against.EntityNotFound(dietPlan, nameof(DietPlan));
            dietPlan.DietPlanMeals = t.DietPlanMeals;
            dietPlan.Name = t.Name;
            await _dietPlanRepository.UpdateAsync(dietPlan);
            return dietPlan;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var spec = new DietPlanByIdFilterSpecification(id);
            var dietPlan = await _dietPlanRepository.FirstOrDefaultAsync(spec);
            Guard.Against.EntityNotFound(dietPlan, nameof(DietPlan));
            await _dietPlanRepository.DeleteAsync(dietPlan);
            return true;
        }
    }
}


