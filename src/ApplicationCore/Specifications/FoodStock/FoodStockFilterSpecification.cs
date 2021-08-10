using ApplicationCore.Entities;
using Ardalis.Specification;

namespace ApplicationCore.Specifications.FoodStockSpecs
{
    public class FoodStockFilterSpecification : Specification<FoodStock>
    {
        public FoodStockFilterSpecification(int? foodProductId)
        {
            Query.Where(i => !foodProductId.HasValue || i.FoodProductId == foodProductId);
        }
    }
}