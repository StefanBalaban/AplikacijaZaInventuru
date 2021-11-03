using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ApplicationCore.Constants;
using ApplicationCore.Entities.UserAggregate;
using ApplicationCore.Filters;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Entities.NotificationAggregate
{
    public class NotificationRule : BaseEntity, IAggregateRoot
    {       

        public FoodProduct FoodProduct { get; set; }
        [Dto] [Get(FilterConstants.EQUAL)] [Post] [Required] public int FoodProductId { get; set; }
        [Dto] [Get(FilterConstants.INCLUDE)] [Post] [Put] [Required] public List<NotificationRuleUserContactInfos> NotificationRuleUserContactInfos { get; set; }
        [Dto] [Get(FilterConstants.EQUAL)] [Post] [Put] [Required] public int UserId { get; set; }
    }
}