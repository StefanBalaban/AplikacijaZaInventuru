using PublicApi.Util;
using System;

namespace PublicApi.Endpoints.NotificationRuleEndpoints
{
    public class DeleteNotificationRuleResponse : BaseResponse
    {
        public DeleteNotificationRuleResponse(Guid correlationId) : base(correlationId)
        {
        }

        public DeleteNotificationRuleResponse()
        {
        }

        public string Status { get; set; } = "Deleted";
    }
}

