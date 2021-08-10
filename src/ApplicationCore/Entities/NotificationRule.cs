using System.Collections.Generic;
using ApplicationCore.Entities.UserAggregate;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Entities
{
    public class NotificationRule : BaseEntity, IAggregateRoot
    {
        public List<UserContactInfo> UserContactInfos { get; set; }

        public FoodProduct FoodProduct { get; set; }
    }
}