using ApplicationCore.Entities.DietPlanAggregate;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Create : BaseAsyncEndpoint.WithRequest<CreateDietPlanRequest>.WithResponse<CreateDietPlanResponse>
    {
        private readonly IDietPlanService _dietPlanService;
        private readonly IMapper _mapper;
        public Create(IDietPlanService dietPlanService, IMapper mapper)
        {
            _dietPlanService = dietPlanService;
            _mapper = mapper;
        }

        [HttpPost("api/dietplan")]
        [SwaggerOperation(Summary = "Create DietPlan", Description = "Create DietPlan", OperationId = "dietplan.create", Tags = new[] { "DietPlanEndpoints" })]
        public override async Task<ActionResult<CreateDietPlanResponse>> HandleAsync(CreateDietPlanRequest request, CancellationToken cancellationToken)
        {
            var response = new CreateDietPlanResponse(request.CorrelationId());
            var dietPlan = await _dietPlanService.PostAsync(new DietPlan { DietPlanMeals = request.DietPlanMeals, Name = request.Name });
            response.DietPlan = _mapper.Map<DietPlanDto>(dietPlan);
            return response;
        }
    }
}
