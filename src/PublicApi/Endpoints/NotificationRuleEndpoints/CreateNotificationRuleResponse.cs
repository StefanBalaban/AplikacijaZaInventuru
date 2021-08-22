using PublicApi.Util;
using System;

namespace PublicApi.Endpoints.NotificationRuleEndpoints
{
    public class CreateNotificationRuleResponse : BaseResponse
    {
        public CreateNotificationRuleResponse(Guid correlationId) : base(correlationId)
        {
        }

        public CreateNotificationRuleResponse()
        {
        }

        public NotificationRuleDto NotificationRule { get; set; }
    }
}

