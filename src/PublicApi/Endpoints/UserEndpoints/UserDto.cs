using ApplicationCore.Entities.UserAggregate;
using System.Collections.Generic;

namespace PublicApi.Endpoints.UserEndpoints
{
    public class UserDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<UserContactInfoDto> UserContactInfos { get; set; }
    }
}

