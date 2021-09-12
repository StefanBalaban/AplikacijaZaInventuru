using ApplicationCore.Entities.UserAggregate;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Create : BaseAsyncEndpoint.WithRequest<CreateUserRequest>.WithResponse<CreateUserResponse>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public Create(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("api/user")]
        [SwaggerOperation(Summary = "Create User", Description = "Create User", OperationId = "user.create", Tags = new[] { "UserEndpoints" })]
        public override async Task<ActionResult<CreateUserResponse>> HandleAsync(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var response = new CreateUserResponse(request.CorrelationId());
            var user = await _userService.PostAsync(new User { FirstName = request.FirstName, LastName = request.LastName, UserContactInfos = request.UserContactInfos });
            response.User = _mapper.Map<UserDto>(user);
            return response;
        }
    }
}

