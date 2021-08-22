using ApplicationCore.Interfaces;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace PublicApi.Endpoints.UserEndpoints
{
    [Authorize(Roles = "Administrators", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GetById : BaseAsyncEndpoint.WithRequest<GetByIdUserRequest>.WithResponse<GetByIdUserResponse>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public GetById(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("api/user/{Id}")]
        [SwaggerOperation(Summary = "GetById User", Description = "GetById User", OperationId = "user.getbyid", Tags = new[] { "UserEndpoints" })]
        public override async Task<ActionResult<GetByIdUserResponse>> HandleAsync([FromRoute] GetByIdUserRequest request, CancellationToken cancellationToken)
        {
            var response = new GetByIdUserResponse(request.CorrelationId());
            var user = await _userService.GetAsync(request.Id);
            response.User = _mapper.Map<UserDto>(user);
            return Ok(response);
        }
    }
}

