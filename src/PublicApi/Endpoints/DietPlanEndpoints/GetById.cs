using ApplicationCore.Interfaces;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace PublicApi.Endpoints.DietPlanEndpoints
{
    [Authorize(Roles = "Administrators", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GetById : BaseAsyncEndpoint.WithRequest<GetByIdDietPlanRequest>.WithResponse<GetByIdDietPlanResponse>
    {
        private readonly IDietPlanService _dietPlanService;
        private readonly IMapper _mapper;
        public GetById(IDietPlanService dietPlanService, IMapper mapper)
        {
            _dietPlanService = dietPlanService;
            _mapper = mapper;
        }

        [HttpGet("api/dietplan/{Id}")]
        [SwaggerOperation(Summary = "GetById DietPlan", Description = "GetById DietPlan", OperationId = "dietplan.getbyid", Tags = new[] { "DietPlanEndpoints" })]
        public override async Task<ActionResult<GetByIdDietPlanResponse>> HandleAsync([FromRoute] GetByIdDietPlanRequest request, CancellationToken cancellationToken)
        {
            var response = new GetByIdDietPlanResponse(request.CorrelationId());
            var dietPlan = await _dietPlanService.GetAsync(request.Id);
            response.DietPlan = _mapper.Map<DietPlanDto>(dietPlan);
            return Ok(response);
        }
    }
}
