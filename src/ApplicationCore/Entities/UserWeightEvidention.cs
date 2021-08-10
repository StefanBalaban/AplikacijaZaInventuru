using System;
using System.ComponentModel.DataAnnotations;
using ApplicationCore.Filters;

namespace ApplicationCore.Entities
{
    public class UserWeightEvidention : BaseEntity
    {
        [Dto] [Get] [Post] [Put] [Required] public int UserId { get; set; }

        [Dto] [Get] [Post] [Put] [Required] public DateTime EvidentationDate { get; set; }

        [Dto] [Get] [Post] [Put] [Required] public float Weight { get; set; }
    }
}