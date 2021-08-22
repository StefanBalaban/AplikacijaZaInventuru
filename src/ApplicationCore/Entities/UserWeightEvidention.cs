using System;
using System.ComponentModel.DataAnnotations;
using ApplicationCore.Filters;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Entities
{
    public class UserWeightEvidention : BaseEntity, IAggregateRoot
    {
        [Dto] [Get] [Post] [Required] public int UserId { get; set; }

        [Dto] [Get] [Post] [Put] [Required] public DateTime EvidentationDate { get; set; }

        [Dto] [Get] [Post] [Put] [Required] public float Weight { get; set; }
    }
}