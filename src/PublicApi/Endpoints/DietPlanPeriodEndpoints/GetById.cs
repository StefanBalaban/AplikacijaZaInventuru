using ApplicationCore.Interfaces;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace PublicApi.Endpoints.DietPlanPeriodEndpoints
{
    [Authorize(Roles = "Administrators", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GetById : BaseAsyncEndpoint.WithRequest<GetByIdDietPlanPeriodRequest>.WithResponse<GetByIdDietPlanPeriodResponse>
    {
        private readonly IDietPlanPeriodService _dietPlanPeriodService;
        private readonly IMapper _mapper;
        public GetById(IDietPlanPeriodService dietPlanPeriodService, IMapper mapper)
        {
            _dietPlanPeriodService = dietPlanPeriodService;
            _mapper = mapper;
        }

        [HttpGet("api/dietplanperiod/{Id}")]
        [SwaggerOperation(Summary = "GetById DietPlanPeriod", Description = "GetById DietPlanPeriod", OperationId = "dietplanperiod.getbyid", Tags = new[] { "DietPlanPeriodEndpoints" })]
        public override async Task<ActionResult<GetByIdDietPlanPeriodResponse>> HandleAsync([FromRoute] GetByIdDietPlanPeriodRequest request, CancellationToken cancellationToken)
        {
            var response = new GetByIdDietPlanPeriodResponse(request.CorrelationId());
            var dietPlanPeriod = await _dietPlanPeriodService.GetAsync(request.Id);
            response.DietPlanPeriod = _mapper.Map<DietPlanPeriodDto>(dietPlanPeriod);
            return Ok(response);
        }
    }
}

