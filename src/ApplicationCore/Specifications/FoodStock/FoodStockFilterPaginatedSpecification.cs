using ApplicationCore.Entities;
using Ardalis.Specification;

namespace ApplicationCore.Specifications.FoodStockSpecs
{
    public class FoodStockFilterPaginatedSpecification : Specification<FoodStock>
    {
        public FoodStockFilterPaginatedSpecification(int skip, int take, int? foodProductId)
        {
            Query.Where(i => !foodProductId.HasValue || i.FoodProductId == foodProductId);
            Query.Skip(skip);
            Query.Take(take);
            Query.Include(x => x.FoodProduct);
        }
    }
}