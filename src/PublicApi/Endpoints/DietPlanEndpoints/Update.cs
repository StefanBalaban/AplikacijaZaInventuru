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
    public class Update : BaseAsyncEndpoint.WithRequest<UpdateDietPlanRequest>.WithResponse<UpdateDietPlanResponse>
    {
        private readonly IDietPlanService _dietPlanService;
        private readonly IMapper _mapper;
        public Update(IDietPlanService dietPlanService, IMapper mapper)
        {
            _dietPlanService = dietPlanService;
            _mapper = mapper;
        }

        [HttpPut("api/dietplan")]
        [SwaggerOperation(Summary = "Update DietPlan", Description = "Update DietPlan", OperationId = "dietplan.update", Tags = new[] { "DietPlanEndpoints" })]
        public override async Task<ActionResult<UpdateDietPlanResponse>> HandleAsync(UpdateDietPlanRequest request, CancellationToken cancellationToken)
        {
            var response = new UpdateDietPlanResponse(request.CorrelationId());
            var dietPlan = await _dietPlanService.PutAsync(new DietPlan { DietPlanMeals = request.DietPlanMeals, Name = request.Name });
            response.DietPlan = _mapper.Map<DietPlanDto>(dietPlan);
            return response;
        }
    }
}
