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
    public class Delete : BaseAsyncEndpoint.WithRequest<DeleteDietPlanRequest>.WithResponse<DeleteDietPlanResponse>
    {
        private readonly IDietPlanService _dietPlanService;
        private readonly IMapper _mapper;
        public Delete(IDietPlanService dietPlanService, IMapper mapper)
        {
            _dietPlanService = dietPlanService;
            _mapper = mapper;
        }

        [HttpDelete("api/dietplan/{Id}")]
        [SwaggerOperation(Summary = "Delete DietPlan", Description = "Delete DietPlan", OperationId = "dietplan.delete", Tags = new[] { "DietPlanEndpoints" })]
        public override async Task<ActionResult<DeleteDietPlanResponse>> HandleAsync([FromRoute] DeleteDietPlanRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteDietPlanResponse(request.CorrelationId());
            var dietPlan = await _dietPlanService.DeleteAsync(request.Id);
            return Ok(response);
        }
    }
}
