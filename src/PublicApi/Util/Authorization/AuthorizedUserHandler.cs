using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications.UserSpecs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace PublicApi.Util.Authorization
{

    public class AuthorizedUserHandler : IAuthorizationHandler
    {

        private readonly IActiveUsersSingleton _activeUsersSingleton;
        public AuthorizedUserHandler(IActiveUsersSingleton activeUsersSingleton)
        {
            _activeUsersSingleton = activeUsersSingleton;
        }
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            var pendingRequirements = context.PendingRequirements.ToList();

            foreach (var requirement in pendingRequirements)
            {
                if (requirement is AuthorizationUserRequirement)
                {
                    if (context.User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "Administrator"))
                    {
                        context.Succeed(requirement);
                    }

                    var name = context.User.Claims.SingleOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;

                    if (context.Resource is HttpContext httpContext && _activeUsersSingleton.ActiveUsersId.TryGetValue(name, out int userId))
                    {
                        var query = httpContext.Request.Query["userId"].ToString();
                        if (query != null && int.TryParse(query, out int userIdQuery) && userIdQuery == userId)
                        {

                            context.Succeed(requirement);
                        }


                    }
                }
            }


            return Task.CompletedTask;
        }
    }
}