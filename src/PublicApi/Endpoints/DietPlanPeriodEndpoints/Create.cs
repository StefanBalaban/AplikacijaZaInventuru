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
    public class Create : BaseAsyncEndpoint.WithRequest<CreateDietPlanPeriodRequest>.WithResponse<CreateDietPlanPeriodResponse>
    {
        private readonly IDietPlanPeriodService _dietPlanPeriodService;
        private readonly IMapper _mapper;
        public Create(IDietPlanPeriodService dietPlanPeriodService, IMapper mapper)
        {
            _dietPlanPeriodService = dietPlanPeriodService;
            _mapper = mapper;
        }

        [HttpPost("api/dietplanperiod")]
        [SwaggerOperation(Summary = "Create DietPlanPeriod", Description = "Create DietPlanPeriod", OperationId = "dietplanperiod.create", Tags = new[] { "DietPlanPeriodEndpoints" })]
        public override async Task<ActionResult<CreateDietPlanPeriodResponse>> HandleAsync(CreateDietPlanPeriodRequest request, CancellationToken cancellationToken)
        {
            var response = new CreateDietPlanPeriodResponse(request.CorrelationId());
            var dietPlanPeriod = await _dietPlanPeriodService.PostAsync(new DietPlanPeriod {UserId = request.UserId, DietPlanId = request.DietPlanId, StartDate = request.StartDate, EndDate = request.EndDate });
            response.DietPlanPeriod = _mapper.Map<DietPlanPeriodDto>(dietPlanPeriod);
            return response;
        }
    }
}

