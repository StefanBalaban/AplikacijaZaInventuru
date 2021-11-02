using System.Security.Claims;
using ApplicationCore.Interfaces;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Specifications.UserSpecs;

namespace PublicApi.Endpoints.UserEndpoints
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GetId : BaseAsyncEndpoint.WithRequest<bool>.WithResponse<GetIdUserResponse>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public GetId(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("api/userid")]
        [SwaggerOperation(Summary = "GetId User", Description = "GetId User", OperationId = "user.getid", Tags = new[] { "UserEndpoints" })]
        public override async Task<ActionResult<GetIdUserResponse>> HandleAsync([FromQuery] bool GetId, CancellationToken cancellationToken)
        {
            var name = User.Claims.SingleOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType);
            if (name == null) return NotFound();
            
            var spec = new UserByNameFilterSpecification(name.Value);
            var response = new GetIdUserResponse();
            var user = await _userService.GetAsync(spec, spec);
            if (user.List.Count == 0) return NotFound();
            response.User = _mapper.Map<UserDto>(user.List[0]);
            return Ok(response);
        }
    }
}

