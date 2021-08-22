using ApplicationCore.Interfaces;
using ApplicationCore.Specifications.UserSpecs;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PublicApi.Endpoints.UserEndpoints
{
    [Authorize(Roles = "Administrators", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ListPaged : BaseAsyncEndpoint.WithRequest<ListPagedUserRequest>.WithResponse<ListPagedUserResponse>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public ListPaged(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("api/user")]
        [SwaggerOperation(Summary = "ListPaged User", Description = "ListPaged User", OperationId = "user.listpaged", Tags = new[] { "UserEndpoints" })]
        public override async Task<ActionResult<ListPagedUserResponse>> HandleAsync([FromQuery] ListPagedUserRequest request, CancellationToken cancellationToken)
        {
            var response = new ListPagedUserResponse(request.CorrelationId());
            var filterSpec = new UserFilterSpecification();
            var pagedSpec = new UserFilterPaginatedSpecification(request.PageIndex * request.PageSize, request.PageSize);
            var users = await _userService.GetAsync(filterSpec, pagedSpec);
            response.Users.AddRange(users.List.Select(_mapper.Map<UserDto>));
            response.PageCount = users.List.Count;
            return Ok(response);
        }
    }
}

