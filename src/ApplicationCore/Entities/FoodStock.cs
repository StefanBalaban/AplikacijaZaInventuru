using System;
using System.ComponentModel.DataAnnotations;
using ApplicationCore.Constants;
using ApplicationCore.Filters;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Entities
{
    public class FoodStock : BaseEntity, IAggregateRoot
    {
        [Get(FilterConstants.INCLUDE)] public FoodProduct FoodProduct { get; set; }

        [Dto]
        [Get(FilterConstants.EQUAL)]
        [Post]
        [Required]
        public int FoodProductId { get; set; }

        [Dto] [Get] [Post] [Put] [Required] public float Amount { get; set; }

        [Dto] [Get] [Post] [Put] public float? UpperAmount { get; set; }

        [Dto] [Get] [Post] [Put] public float? LowerAmount { get; set; }

        [Dto] [Get] [Post] [Put] public DateTime? DateOfPurchase { get; set; }

        [Dto] [Get] [Post] [Put] public DateTime? BestUseByDate { get; set; }
    }
}