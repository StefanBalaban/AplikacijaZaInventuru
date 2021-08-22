using ApplicationCore.Interfaces;
using ApplicationCore.Specifications.UserWeightEvidentionSpecs;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PublicApi.Endpoints.UserWeightEvidentionEndpoints
{
    [Authorize(Roles = "Administrators", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ListPaged : BaseAsyncEndpoint.WithRequest<ListPagedUserWeightEvidentionRequest>.WithResponse<ListPagedUserWeightEvidentionResponse>
    {
        private readonly IUserWeightEvidentionService _userWeightEvidentionService;
        private readonly IMapper _mapper;
        public ListPaged(IUserWeightEvidentionService userWeightEvidentionService, IMapper mapper)
        {
            _userWeightEvidentionService = userWeightEvidentionService;
            _mapper = mapper;
        }

        [HttpGet("api/userweightevidention")]
        [SwaggerOperation(Summary = "ListPaged UserWeightEvidention", Description = "ListPaged UserWeightEvidention", OperationId = "userweightevidention.listpaged", Tags = new[] { "UserWeightEvidentionEndpoints" })]
        public override async Task<ActionResult<ListPagedUserWeightEvidentionResponse>> HandleAsync([FromQuery] ListPagedUserWeightEvidentionRequest request, CancellationToken cancellationToken)
        {
            var response = new ListPagedUserWeightEvidentionResponse(request.CorrelationId());
            var filterSpec = new UserWeightEvidentionFilterSpecification();
            var pagedSpec = new UserWeightEvidentionFilterPaginatedSpecification(request.PageIndex * request.PageSize, request.PageSize);
            var userWeightEvidentions = await _userWeightEvidentionService.GetAsync(filterSpec, pagedSpec);
            response.UserWeightEvidentions.AddRange(userWeightEvidentions.List.Select(_mapper.Map<UserWeightEvidentionDto>));
            response.PageCount = userWeightEvidentions.List.Count;
            return Ok(response);
        }
    }
}
