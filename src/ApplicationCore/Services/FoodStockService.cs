using ApplicationCore.Entities;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using Ardalis.Specification;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class FoodStockService : IFoodStockService
    {
        private readonly IAsyncRepository<FoodStock> _foodStockRepository;
        private readonly IAppLogger<FoodStockService> _logger;

        public FoodStockService(IAsyncRepository<FoodStock> foodStockRepository, IAppLogger<FoodStockService> logger)
        {
            _foodStockRepository = foodStockRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<FoodStock>> GetAsync()
        {
            return await _foodStockRepository.ListAllAsync();
        }

        public async Task<FoodStock> GetAsync(int id)
        {
            var foodStock = await _foodStockRepository.GetByIdAsync(id);
            Guard.Against.EntityNotFound(foodStock, nameof(FoodStock));
            return foodStock;
        }

        public async Task<ListEntity<FoodStock>> GetAsync(Specification<FoodStock> filterSpec,
            Specification<FoodStock> pagedSpec)
        {
            return new ListEntity<FoodStock>
            {
                List = await _foodStockRepository.ListAsync(pagedSpec),
                Count = await _foodStockRepository.CountAsync(filterSpec)
            };
        }

        public async Task<FoodStock> PostAsync(FoodStock t)
        {
            Guard.Against.ModelStateIsInvalid(t, nameof(FoodStock));
            return await _foodStockRepository.AddAsync(new FoodStock
            {
                FoodProductId = t.FoodProductId,
                Amount = t.Amount,
                UpperAmount = t.UpperAmount,
                LowerAmount = t.LowerAmount,
                DateOfPurchase = t.DateOfPurchase,
                BestUseByDate = t.BestUseByDate
            });
        }

        public async Task<FoodStock> PutAsync(FoodStock t)
        {
            Guard.Against.ModelStateIsInvalid(t, nameof(FoodStock));
            var foodStock = await _foodStockRepository.GetByIdAsync(t.Id);
            Guard.Against.EntityNotFound(foodStock, nameof(FoodStock));
            foodStock.Amount = t.Amount;
            foodStock.UpperAmount = t.UpperAmount;
            foodStock.LowerAmount = t.LowerAmount;
            foodStock.DateOfPurchase = t.DateOfPurchase;
            foodStock.BestUseByDate = t.BestUseByDate;
            await _foodStockRepository.UpdateAsync(foodStock);
            return foodStock;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var foodStock = await _foodStockRepository.GetByIdAsync(id);
            Guard.Against.EntityNotFound(foodStock, nameof(FoodStock));
            await _foodStockRepository.DeleteAsync(foodStock);
            return true;
        }
    }
}