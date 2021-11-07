using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications.UserSpecs;
using Microsoft.AspNetCore.Http;

namespace PublicApi.Util.Middleware
{
    public class ActiveUsersMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly IActiveUsersSingleton _activeUsersSingleton;
        private IUserService _userService;

        public ActiveUsersMiddleWare(RequestDelegate next, IActiveUsersSingleton activeUsersSingleton)
        {
            _next = next;
            _activeUsersSingleton = activeUsersSingleton;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            _userService = userService;
            var name = context.User.Claims.SingleOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType)?.Value;

            if (name != null && !_activeUsersSingleton.ActiveUsersId.TryGetValue(name, out int userId))
            {
                var spec = new UserByNameFilterSpecification(name);
                var list = await _userService.GetAsync(spec, spec);
                var user = list.List.SingleOrDefault(x => x.FirstName.Equals(name));
                if (user != null)
                {
                    _activeUsersSingleton.ActiveUsersId.Add(name, user.Id);
                }
            }

            await _next(context);
        }
    }
}