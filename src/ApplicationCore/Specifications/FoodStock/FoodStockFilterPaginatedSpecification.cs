using ApplicationCore.Entities;
using Ardalis.Specification;

namespace ApplicationCore.Specifications.FoodStockSpecs
{
    public class FoodStockFilterPaginatedSpecification : Specification<FoodStock>
    {
        public FoodStockFilterPaginatedSpecification(int? userId, int skip, int take, int? foodProductId)
        {
            Query.Where(i => !foodProductId.HasValue || i.FoodProductId == foodProductId);
            Query.Skip(skip);
            Query.Where(i => userId == null || i.UserId == userId);
            Query.Take(take);
            Query.Include(x => x.FoodProduct);
        }
    }
}