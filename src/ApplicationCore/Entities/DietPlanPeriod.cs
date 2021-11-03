using ApplicationCore.Constants;
using ApplicationCore.Entities.DietPlanAggregate;
using ApplicationCore.Filters;
using ApplicationCore.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    public class DietPlanPeriod : BaseEntity, IAggregateRoot
    {
        
        public DietPlan DietPlan { get; set; }
        [Dto]
        [Post]
        [Put]
        [Required]
        public int DietPlanId { get; set; }
        [Dto]
        [Get(FilterConstants.GTE, FilterConstants.LTE)]
        [Post]
        [Put]
        [Required]
        public DateTime StartDate { get; set; }
        [Dto]
        [Get(FilterConstants.GTE, FilterConstants.LTE)]
        [Post]
        [Put]
        [Required]
        public DateTime EndDate { get; set; }
        [Dto] [Get(FilterConstants.EQUAL)] [Post] [Put] [Required] public int UserId { get; set; }
    }
}