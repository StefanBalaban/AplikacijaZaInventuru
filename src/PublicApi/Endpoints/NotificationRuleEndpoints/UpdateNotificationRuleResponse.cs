using PublicApi.Util;
using System;

namespace PublicApi.Endpoints.NotificationRuleEndpoints
{
    public class UpdateNotificationRuleResponse : BaseResponse
    {
        public UpdateNotificationRuleResponse(Guid correlationId) : base(correlationId)
        {
        }

        public UpdateNotificationRuleResponse()
        {
        }

        public NotificationRuleDto NotificationRule { get; set; }
    }
}

