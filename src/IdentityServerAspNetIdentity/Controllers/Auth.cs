using IdentityServer4;
using IdentityServerAspNetIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Auth : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IdentityServerTools _tools;
        private readonly RoleManager<IdentityRole> _roleManager;

        public Auth(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IdentityServerTools tools, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tools = tools;
            _roleManager = roleManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> SignIn(AuthRequest request)
        {
            var response = new AuthResponse();

            var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, lockoutOnFailure: true);

            response.Result = result.Succeeded;
            response.IsLockedOut = result.IsLockedOut;
            response.IsNotAllowed = result.IsNotAllowed;
            response.RequiresTwoFactor = result.RequiresTwoFactor;
            response.Username = request.Username;

            if (result.Succeeded)
            {                
                var user = await _userManager.FindByNameAsync(request.Username);
                var roles = await _userManager.GetRolesAsync(user);
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, request.Username) };

                foreach (var role in roles) claims.Add(new Claim("Role", role));

                response.Token = await _tools.IssueClientJwtAsync("client",3600, scopes: new[] { "api1"}, audiences: new[] { "api1" }, additionalClaims: claims);

                return Ok(response);
            }
            return BadRequest("Log in failed");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AuthRequest request)
        {
            var user = new ApplicationUser { UserName = request.Email, Email = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);
            var response = new AuthResponse();
            response.Result = result.Succeeded;
            response.Username = request.Username;

            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, request.Username) };

                foreach (var role in roles) claims.Add(new Claim("Role", role));

                response.Token = await _tools.IssueClientJwtAsync("client", 3600, scopes: new[] { "api1" }, audiences: new[] { "api1" }, additionalClaims: claims);

                return Ok(response);
            }
            return BadRequest(result.Errors);

        }

        [HttpPost("AddUsersRoles")]
        [Authorize(Roles ="Administrators")]
        public async Task<IActionResult> AddUsersRoles(UserRolesRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            var result = await _userManager.AddToRolesAsync(user, request.Roles);
            var response = new AuthResponse();
            response.Result = result.Succeeded;
            response.Username = request.Username;

            if (result.Succeeded)
            {           

                return Ok(response);
            }
            return BadRequest(result.Errors);

        }
        [HttpPost("RemoveUsersRoles")]
        [Authorize(Roles = "Administrators")]
        public async Task<IActionResult> RemoveUsersRoles(UserRolesRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            var result = await _userManager.RemoveFromRolesAsync(user, request.Roles);
            var response = new AuthResponse();
            response.Result = result.Succeeded;
            response.Username = request.Username;

            if (result.Succeeded)
            {

                return Ok(response);
            }
            return BadRequest(result.Errors);

        }
        [HttpPost("AddRole")]
        [Authorize(Roles = "Administrators")]
        public async Task<IActionResult> AddRole(RoleRequest request)
        {           
            
            var result = await _roleManager.CreateAsync(new IdentityRole() { Name = request.Name});
            var response = new AuthResponse();
            response.Result = result.Succeeded;            

            if (result.Succeeded)
            {

                return Ok(response);
            }
            return BadRequest(result.Errors);

        }
        [HttpPost("RemoveRole")]
        [Authorize(Roles = "Administrators")]
        public async Task<IActionResult> RemoveRole(RoleRequest request)
        {
            var result = await _roleManager.FindByNameAsync(request.Name);
            if (result == null) return NotFound();

            await _roleManager.DeleteAsync(result);


            return Ok();


        }
        [HttpPost("GetRoles")]
        [Authorize(Roles = "Administrators")]
        public async Task<IActionResult> GetRoles(UserRolesRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null) return NotFound();
            var result = await _userManager.GetRolesAsync(user);
            var response = new RolesResponse();
            response.Roles = result;
            response.Username = request.Username;
            return Ok(response);
        }
    }
}
