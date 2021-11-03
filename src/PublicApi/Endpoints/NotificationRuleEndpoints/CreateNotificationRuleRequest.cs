using ApplicationCore.Entities.NotificationAggregate;
using PublicApi.Util;
using System.Collections.Generic;

namespace PublicApi.Endpoints.NotificationRuleEndpoints
{
    public class CreateNotificationRuleRequest : BaseRequest 
    {
        public int UserId { get; set; }
        public int FoodProductId { get; set; }

        public List<NotificationRuleUserContactInfos> NotificationRuleUserContactInfos { get; set; }
    }
}

