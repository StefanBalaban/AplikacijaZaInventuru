using PublicApi.Util;

namespace PublicApi.Endpoints.NotificationRuleEndpoints
{
    public class ListPagedNotificationRuleRequest : BaseRequest
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int? FoodProductId { get; set; }
    }
}

