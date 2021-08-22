using ApplicationCore.Entities.UserAggregate;
using PublicApi.Util;
using System.Collections.Generic;

namespace PublicApi.Endpoints.UserEndpoints
{
    public class UpdateUserRequest : BaseRequest
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<UserContactInfo> UserContactInfos { get; set; }
    }
}

