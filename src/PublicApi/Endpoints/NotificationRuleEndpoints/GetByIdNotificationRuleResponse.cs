using PublicApi.Util;
using System;

namespace PublicApi.Endpoints.NotificationRuleEndpoints
{
    public class GetByIdNotificationRuleResponse : BaseResponse
    {
        public GetByIdNotificationRuleResponse(Guid correlationId) : base(correlationId)
        {
        }

        public GetByIdNotificationRuleResponse()
        {
        }

        public NotificationRuleDto NotificationRule { get; set; }
    }
}

