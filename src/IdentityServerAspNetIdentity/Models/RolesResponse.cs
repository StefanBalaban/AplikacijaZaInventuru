using System.Collections.Generic;

namespace IdentityServerAspNetIdentity.Models
{
    public class RolesResponse
    {
        public RolesResponse()
        { 
        }
        public string Username { get; set; }
        public IList<string> Roles { get; set; }
    }
}
