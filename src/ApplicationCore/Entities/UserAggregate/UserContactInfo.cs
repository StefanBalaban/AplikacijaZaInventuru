using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ApplicationCore.Entities.NotificationAggregate;
using ApplicationCore.Filters;

namespace ApplicationCore.Entities.UserAggregate
{
    public class UserContactInfo : BaseEntity
    {
        public User User { get; set; }
        [Dto] [Get] [Post] [Required] public int UserId { get; set; }
        [Dto] [Get] [Post] [Put] [Required] public string Contact { get; set; }
        public List<NotificationRuleUserContactInfos> NotificationRuleUserContactInfos { get; set; }
    }
}