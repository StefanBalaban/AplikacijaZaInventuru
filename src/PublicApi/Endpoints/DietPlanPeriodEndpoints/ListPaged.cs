using ApplicationCore.Interfaces;
using ApplicationCore.Specifications.DietPlanPeriodSpecs;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PublicApi.Endpoints.DietPlanPeriodEndpoints
{
    [Authorize(Roles = "Administrators", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ListPaged : BaseAsyncEndpoint.WithRequest<ListPagedDietPlanPeriodRequest>.WithResponse<ListPagedDietPlanPeriodResponse>
    {
        private readonly IDietPlanPeriodService _dietPlanPeriodService;
        private readonly IMapper _mapper;
        public ListPaged(IDietPlanPeriodService dietPlanPeriodService, IMapper mapper)
        {
            _dietPlanPeriodService = dietPlanPeriodService;
            _mapper = mapper;
        }

        [HttpGet("api/dietplanperiod")]
        [SwaggerOperation(Summary = "ListPaged DietPlanPeriod", Description = "ListPaged DietPlanPeriod", OperationId = "dietplanperiod.listpaged", Tags = new[] { "DietPlanPeriodEndpoints" })]
        public override async Task<ActionResult<ListPagedDietPlanPeriodResponse>> HandleAsync([FromQuery] ListPagedDietPlanPeriodRequest request, CancellationToken cancellationToken)
        {
            var response = new ListPagedDietPlanPeriodResponse(request.CorrelationId());
            var filterSpec = new DietPlanPeriodFilterSpecification(request.StartDateGTE, request.StartDateLTE, request.EndDateGTE, request.EndDateLTE);
            var pagedSpec = new DietPlanPeriodFilterPaginatedSpecification(request.PageIndex * request.PageSize, request.PageSize, request.StartDateGTE, request.StartDateLTE, request.EndDateGTE, request.EndDateLTE);
            var dietPlanPeriods = await _dietPlanPeriodService.GetAsync(filterSpec, pagedSpec);
            response.DietPlanPeriods.AddRange(dietPlanPeriods.List.Select(_mapper.Map<DietPlanPeriodDto>));
            response.PageCount = dietPlanPeriods.List.Count;
            return Ok(response);
        }
    }
}

