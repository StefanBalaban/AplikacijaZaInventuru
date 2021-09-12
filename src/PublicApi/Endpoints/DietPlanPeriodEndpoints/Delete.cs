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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Delete : BaseAsyncEndpoint.WithRequest<DeleteDietPlanPeriodRequest>.WithResponse<DeleteDietPlanPeriodResponse>
    {
        private readonly IDietPlanPeriodService _dietPlanPeriodService;
        private readonly IMapper _mapper;
        public Delete(IDietPlanPeriodService dietPlanPeriodService, IMapper mapper)
        {
            _dietPlanPeriodService = dietPlanPeriodService;
            _mapper = mapper;
        }

        [HttpDelete("api/dietplanperiod/{Id}")]
        [SwaggerOperation(Summary = "Delete DietPlanPeriod", Description = "Delete DietPlanPeriod", OperationId = "dietplanperiod.delete", Tags = new[] { "DietPlanPeriodEndpoints" })]
        public override async Task<ActionResult<DeleteDietPlanPeriodResponse>> HandleAsync([FromRoute] DeleteDietPlanPeriodRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteDietPlanPeriodResponse(request.CorrelationId());
            var dietPlanPeriod = await _dietPlanPeriodService.DeleteAsync(request.Id);
            return Ok(response);
        }
    }
}

