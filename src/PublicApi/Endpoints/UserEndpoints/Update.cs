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
    [Authorize(Roles = "Administrators", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Update : BaseAsyncEndpoint.WithRequest<UpdateUserRequest>.WithResponse<UpdateUserResponse>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public Update(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPut("api/user")]
        [SwaggerOperation(Summary = "Update User", Description = "Update User", OperationId = "user.update", Tags = new[] { "UserEndpoints" })]
        public override async Task<ActionResult<UpdateUserResponse>> HandleAsync(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var response = new UpdateUserResponse(request.CorrelationId());
            var user = await _userService.PutAsync(new User { FirstName = request.FirstName, LastName = request.LastName, UserContactInfos = request.UserContactInfos });
            response.User = _mapper.Map<UserDto>(user);
            return response;
        }
    }
}

