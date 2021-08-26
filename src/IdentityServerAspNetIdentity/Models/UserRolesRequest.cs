using System.Collections.Generic;

namespace IdentityServerAspNetIdentity.Models
{
    public class UserRolesRequest
    {
        public string Username { get; set; }
        public List<string> Roles { get; set; }
    }
}