using PublicApi.Util;

namespace PublicApi.Endpoints.NotificationRuleEndpoints
{
    public class GetByIdNotificationRuleRequest : BaseRequest
    {
        public int Id { get; set; }
    }
}

