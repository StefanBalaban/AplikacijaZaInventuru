using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities.MealAggregate;
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
    public class Create : BaseAsyncEndpoint.WithRequest<CreateMealRequest>.WithResponse<CreateMealResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMealService _mealService;

        public Create(IMealService mealService, IMapper mapper)
        {
            _mealService = mealService;
            _mapper = mapper;
        }

        [HttpPost("api/meal")]
        [SwaggerOperation(Summary = "Create Meal", Description = "Create Meal", OperationId = "meal.create",
            Tags = new[] { "MealEndpoints" })]
        public override async Task<ActionResult<CreateMealResponse>> HandleAsync(CreateMealRequest request,
            CancellationToken cancellationToken)
        {
            var response = new CreateMealResponse(request.CorrelationId());
            var meal = await _mealService.PostAsync(new Meal {UserId = request.UserId, Meals = request.Meals, Name = request.Name });
            response.Meal = _mapper.Map<MealDto>(meal);
            return response;
        }
    }
}