using System.ComponentModel.DataAnnotations;
using ApplicationCore.Filters;

namespace ApplicationCore.Entities.MealAggregate
{
    public class MealItem : BaseEntity
    {
        [Dto] [Get] [Post] [Put] [Required] public FoodProduct FoodProduct { get; set; }

        [Dto] [Get] [Post] [Put] [Required] public float Amount { get; set; }
    }
}