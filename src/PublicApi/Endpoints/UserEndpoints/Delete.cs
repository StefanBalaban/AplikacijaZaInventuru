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
    public class Delete : BaseAsyncEndpoint.WithRequest<DeleteUserRequest>.WithResponse<DeleteUserResponse>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public Delete(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpDelete("api/user/{Id}")]
        [SwaggerOperation(Summary = "Delete User", Description = "Delete User", OperationId = "user.delete", Tags = new[] { "UserEndpoints" })]
        public override async Task<ActionResult<DeleteUserResponse>> HandleAsync([FromRoute] DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteUserResponse(request.CorrelationId());
            var user = await _userService.DeleteAsync(request.Id);
            return Ok(response);
        }
    }
}

