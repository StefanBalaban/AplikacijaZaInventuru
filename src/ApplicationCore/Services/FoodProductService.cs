

using Ardalis.GuardClauses;
using Ardalis.Specification;
using ApplicationCore.Entities;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class FoodProductService : IFoodProductService
    {
        private readonly IAsyncRepository<FoodProduct> _foodProductRepository;
        private readonly IAppLogger<FoodProductService> _logger;
        public FoodProductService(IAsyncRepository<FoodProduct> foodProductRepository, IAppLogger<FoodProductService> logger)
        {
            _foodProductRepository = foodProductRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<FoodProduct>> GetAsync()
        {
            return await _foodProductRepository.ListAllAsync();
        }

        public async Task<FoodProduct> GetAsync(int id)
        {
            var foodProduct = await _foodProductRepository.GetByIdAsync(id);
            Guard.Against.EntityNotFound(foodProduct, nameof(FoodProduct));
            return foodProduct;
        }

        public async Task<ListEntity<FoodProduct>> GetAsync(Specification<FoodProduct> filterSpec, Specification<FoodProduct> pagedSpec)
        {
            return new ListEntity<FoodProduct> { List = await _foodProductRepository.ListAsync(pagedSpec), Count = await _foodProductRepository.CountAsync(filterSpec) };
        }

        public async Task<FoodProduct> PostAsync(FoodProduct t)
        {
            Guard.Against.ModelStateIsInvalid(t, nameof(FoodProduct));
            return await _foodProductRepository.AddAsync(new FoodProduct { Name = t.Name, UnitOfMeasureId = t.UnitOfMeasureId, Calories = t.Calories, Protein = t.Protein, Carbohydrates = t.Carbohydrates, Fats = t.Fats });
        }

        public async Task<FoodProduct> PutAsync(FoodProduct t)
        {
            Guard.Against.ModelStateIsInvalid(t, nameof(FoodProduct));
            var foodProduct = await _foodProductRepository.GetByIdAsync(t.Id);
            Guard.Against.EntityNotFound(foodProduct, nameof(FoodProduct));
            foodProduct.Name = t.Name;
            foodProduct.UnitOfMeasureId = t.UnitOfMeasureId;
            foodProduct.Calories = t.Calories;
            foodProduct.Protein = t.Protein;
            foodProduct.Carbohydrates = t.Carbohydrates;
            await _foodProductRepository.UpdateAsync(foodProduct);
            return foodProduct;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var foodProduct = await _foodProductRepository.GetByIdAsync(id);
            Guard.Against.EntityNotFound(foodProduct, nameof(FoodProduct));
            await _foodProductRepository.DeleteAsync(foodProduct);
            return true;
        }
    }
}