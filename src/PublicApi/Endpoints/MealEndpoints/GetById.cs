using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PublicApi.Endpoints.MealEndpoints
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GetById : BaseAsyncEndpoint.WithRequest<GetByIdMealRequest>.WithResponse<GetByIdMealResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMealService _mealService;

        public GetById(IMealService mealService, IMapper mapper)
        {
            _mealService = mealService;
            _mapper = mapper;
        }

        [HttpGet("api/meal/{Id}")]
        [SwaggerOperation(Summary = "GetById Meal", Description = "GetById Meal", OperationId = "meal.getbyid",
            Tags = new[] { "MealEndpoints" })]
        public override async Task<ActionResult<GetByIdMealResponse>> HandleAsync(
            [FromRoute] GetByIdMealRequest request, CancellationToken cancellationToken)
        {
            var response = new GetByIdMealResponse(request.CorrelationId());
            var meal = await _mealService.GetAsync(request.Id);
            response.Meal = _mapper.Map<MealDto>(meal);
            return Ok(response);
        }
    }
}