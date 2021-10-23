using ApplicationCore.Entities;
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
    public class Update : BaseAsyncEndpoint.WithRequest<UpdateDietPlanPeriodRequest>.WithResponse<UpdateDietPlanPeriodResponse>
    {
        private readonly IDietPlanPeriodService _dietPlanPeriodService;
        private readonly IMapper _mapper;
        public Update(IDietPlanPeriodService dietPlanPeriodService, IMapper mapper)
        {
            _dietPlanPeriodService = dietPlanPeriodService;
            _mapper = mapper;
        }

        [HttpPut("api/dietplanperiod")]
        [SwaggerOperation(Summary = "Update DietPlanPeriod", Description = "Update DietPlanPeriod", OperationId = "dietplanperiod.update", Tags = new[] { "DietPlanPeriodEndpoints" })]
        public override async Task<ActionResult<UpdateDietPlanPeriodResponse>> HandleAsync(UpdateDietPlanPeriodRequest request, CancellationToken cancellationToken)
        {
            var response = new UpdateDietPlanPeriodResponse(request.CorrelationId());
            var dietPlanPeriod = await _dietPlanPeriodService.PutAsync(new DietPlanPeriod { DietPlanId = request.DietPlanId, StartDate = request.StartDate, EndDate = request.EndDate, Id = request.Id });
            response.DietPlanPeriod = _mapper.Map<DietPlanPeriodDto>(dietPlanPeriod);
            return response;
        }
    }
}

