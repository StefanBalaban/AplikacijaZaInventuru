using System;
using System.ComponentModel.DataAnnotations;
using ApplicationCore.Filters;

namespace ApplicationCore.Entities
{
    public class UserSubscription : BaseEntity
    {
        [Dto] [Get] [Post] [Put] [Required] public int UserId { get; set; }

        [Dto] [Get] [Post] [Put] [Required] public DateTime PaymentDate { get; set; }

        [Dto] [Get] [Post] [Put] [Required] public DateTime BegginDate { get; set; }

        [Dto] [Get] [Post] [Put] [Required] public DateTime EndDate { get; set; }
    }
}