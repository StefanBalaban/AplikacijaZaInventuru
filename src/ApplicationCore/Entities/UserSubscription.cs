using ApplicationCore.Constants;
using ApplicationCore.Filters;
using ApplicationCore.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    public class UserSubscription : BaseEntity, IAggregateRoot
    {
        [Dto] [Get] [Post] [Required] public int UserId { get; set; }

        [Dto] [Get] [Post] [Put] [Required] public DateTime PaymentDate { get; set; }

        [Dto] [Get(FilterConstants.GTE, FilterConstants.LTE)] [Post] [Put] [Required] public DateTime BegginDate { get; set; }

        [Dto] [Get(FilterConstants.GTE, FilterConstants.LTE)] [Post] [Put] [Required] public DateTime EndDate { get; set; }
    }
}