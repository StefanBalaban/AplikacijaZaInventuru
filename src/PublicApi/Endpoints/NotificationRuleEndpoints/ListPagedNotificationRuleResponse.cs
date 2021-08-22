using PublicApi.Util;
using System;
using System.Collections.Generic;

namespace PublicApi.Endpoints.NotificationRuleEndpoints
{
    public class ListPagedNotificationRuleResponse : BaseResponse
    {
        public ListPagedNotificationRuleResponse(Guid correlationId) : base(correlationId)
        {
        }

        public ListPagedNotificationRuleResponse()
        {
        }

        public List<NotificationRuleDto> NotificationRules { get; set; } = new List<NotificationRuleDto>();
        public int PageCount { get; set; }
    }
}

