using System.ComponentModel.DataAnnotations;
using ApplicationCore.Filters;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Entities.MealAggregate
{
    public class MealItem : BaseEntity, IAggregateRoot
    {
        [Dto] [Get] [Post] [Put]  public FoodProduct FoodProduct { get; set; }
        [Required] public int FoodProductId { get; set; }

        [Dto] [Get] [Post] [Put] [Required] public float Amount { get; set; }
    }
}