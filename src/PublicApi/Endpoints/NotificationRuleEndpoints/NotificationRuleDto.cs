using ApplicationCore.Entities.NotificationAggregate;
using System.Collections.Generic;

namespace PublicApi.Endpoints.NotificationRuleEndpoints
{
    public class NotificationRuleDto
    {
        public int Id { get; set; }

        public int FoodProductId { get; set; }

        public List<NotificationRuleUserContactInfosDto> NotificationRuleUserContactInfos { get; set; } = new List<NotificationRuleUserContactInfosDto>();
    }
}

