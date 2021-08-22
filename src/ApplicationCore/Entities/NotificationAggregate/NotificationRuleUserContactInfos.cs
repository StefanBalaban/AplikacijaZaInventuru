using ApplicationCore.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities.NotificationAggregate
{
    public class NotificationRuleUserContactInfos
    {
        public UserContactInfo UserContactInfo { get; set; }
        public NotificationRule NotificationRule { get; set; }
        public int UserContactInfosId { get; set; }
        public int NotificationRuleId { get; set; }
    }
}
