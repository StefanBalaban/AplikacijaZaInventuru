using ApplicationCore.Entities.NotificationAggregate;
using PublicApi.Util;
using System.Collections.Generic;

namespace PublicApi.Endpoints.NotificationRuleEndpoints
{
    public class UpdateNotificationRuleRequest : BaseRequest
    {
        public int Id { get; set; }

        public List<NotificationRuleUserContactInfos> NotificationRuleUserContactInfos { get; set; }
    }
}

