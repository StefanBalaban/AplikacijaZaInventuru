﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Entities.MealAggregate;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using Ardalis.Specification;

namespace ApplicationCore.Services
{
    public class MealService : IMealService
    {
        private readonly IAppLogger<MealService> _logger;
        private readonly IAsyncRepository<Meal> _mealRepository;

        public MealService(IAsyncRepository<Meal> mealRepository, IAppLogger<MealService> logger)
        {
            _mealRepository = mealRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Meal>> GetAsync()
        {
            return await _mealRepository.ListAllAsync();
        }

        public async Task<Meal> GetAsync(int id)
        {
            var meal = await _mealRepository.GetByIdAsync(id);
            Guard.Against.EntityNotFound(meal, nameof(Meal));
            return meal;
        }

        public async Task<ListEntity<Meal>> GetAsync(Specification<Meal> filterSpec, Specification<Meal> pagedSpec)
        {
            return new ListEntity<Meal>
            {
                List = await _mealRepository.ListAsync(pagedSpec), Count = await _mealRepository.CountAsync(filterSpec)
            };
        }

        public async Task<Meal> PostAsync(Meal t)
        {
            Guard.Against.ModelStateIsInvalid(t, nameof(Meal));
            return await _mealRepository.AddAsync(new Meal { Meals = t.Meals, Name = t.Name });
        }

        public async Task<Meal> PutAsync(Meal t)
        {
            Guard.Against.ModelStateIsInvalid(t, nameof(Meal));
            var meal = await _mealRepository.GetByIdAsync(t.Id);
            Guard.Against.EntityNotFound(meal, nameof(Meal));
            meal.Meals = t.Meals;
            meal.Name = t.Name;
            await _mealRepository.UpdateAsync(meal);
            return meal;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var meal = await _mealRepository.GetByIdAsync(id);
            Guard.Against.EntityNotFound(meal, nameof(Meal));
            await _mealRepository.DeleteAsync(meal);
            return true;
        }
    }
}