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
    [Authorize(Roles = "Administrators", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Update : BaseAsyncEndpoint.WithRequest<UpdateMealRequest>.WithResponse<UpdateMealResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMealService _mealService;

        public Update(IMealService mealService, IMapper mapper)
        {
            _mealService = mealService;
            _mapper = mapper;
        }

        [HttpPut("api/meal")]
        [SwaggerOperation(Summary = "Update Meal", Description = "Update Meal", OperationId = "meal.update",
            Tags = new[] { "MealEndpoints" })]
        public override async Task<ActionResult<UpdateMealResponse>> HandleAsync(UpdateMealRequest request,
            CancellationToken cancellationToken)
        {
            var response = new UpdateMealResponse(request.CorrelationId());
            var meal = await _mealService.PutAsync(new Meal { Meals = request.Meals, Name = request.Name });
            response.Meal = _mapper.Map<MealDto>(meal);
            return response;
        }
    }
}