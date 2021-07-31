using Ardalis.GuardClauses;
using ApplicationCore.Constants;
using ApplicationCore.Filters;
using ApplicationCore.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    public class FoodProduct : BaseEntity, IAggregateRoot
    {
        [Dto]
        [Get]
        [Post]
        [Put]
        [Required]
        public string Name { get; set; }

        [Get(FilterConstants.INCLUDE)]
        public UnitOfMeasure UnitOfMeasure { get; set; }

        [Get(FilterConstants.EQUAL)]
        [Post]
        [Put]
        [Required]
        [Dto]
        [Range(1, int.MaxValue)]
        public int UnitOfMeasureId { get; set; }

        [Dto]
        [Get(FilterConstants.GTE, FilterConstants.LTE)]
        [Post]
        [Put]
        public float Calories { get; set; }

        [Dto]
        [Get(FilterConstants.EQUAL)]
        [Post]
        [Put]
        public float Protein { get; set; }

        [Post]
        [Put]
        [Dto]
        public float Carbohydrates { get; set; }

        [Get]
        [Post]
        [Dto]
        public float Fats { get; set; }

        public FoodProduct()
        {
        }

        public void EditNutritionalValue(float calories, float protein, float carbohydrates, float fats)
        {
            Calories = calories;
            Protein = protein;
            Carbohydrates = carbohydrates;
            Fats = fats;
        }

        public void EditName(string name)
        {
            Guard.Against.Null(name, nameof(name));

            Name = name;
        }
    }
}