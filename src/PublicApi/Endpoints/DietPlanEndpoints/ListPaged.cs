using ApplicationCore.Interfaces;
using ApplicationCore.Specifications.DietPlanSpecs;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PublicApi.Endpoints.DietPlanEndpoints
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ListPaged : BaseAsyncEndpoint.WithRequest<ListPagedDietPlanRequest>.WithResponse<ListPagedDietPlanResponse>
    {
        private readonly IDietPlanService _dietPlanService;
        private readonly IMapper _mapper;
        public ListPaged(IDietPlanService dietPlanService, IMapper mapper)
        {
            _dietPlanService = dietPlanService;
            _mapper = mapper;
        }

        [HttpGet("api/dietplan")]
        [SwaggerOperation(Summary = "ListPaged DietPlan", Description = "ListPaged DietPlan", OperationId = "dietplan.listpaged", Tags = new[] { "DietPlanEndpoints" })]
        public override async Task<ActionResult<ListPagedDietPlanResponse>> HandleAsync([FromQuery] ListPagedDietPlanRequest request, CancellationToken cancellationToken)
        {
            var response = new ListPagedDietPlanResponse(request.CorrelationId());
            var filterSpec = new DietPlanFilterSpecification(request.Name);
            var pagedSpec = new DietPlanFilterPaginatedSpecification(request.PageIndex * request.PageSize, request.PageSize, request.Name);
            var dietPlans = await _dietPlanService.GetAsync(filterSpec, pagedSpec);
            response.DietPlans.AddRange(dietPlans.List.Select(_mapper.Map<DietPlanDto>));
            response.PageCount = dietPlans.List.Count;
            return Ok(response);
        }
    }
}
