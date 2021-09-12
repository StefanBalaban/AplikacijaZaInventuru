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
    public class Delete : BaseAsyncEndpoint.WithRequest<DeleteMealRequest>.WithResponse<DeleteMealResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMealService _mealService;

        public Delete(IMealService mealService, IMapper mapper)
        {
            _mealService = mealService;
            _mapper = mapper;
        }

        [HttpDelete("api/meal/{Id}")]
        [SwaggerOperation(Summary = "Delete Meal", Description = "Delete Meal", OperationId = "meal.delete",
            Tags = new[] { "MealEndpoints" })]
        public override async Task<ActionResult<DeleteMealResponse>> HandleAsync([FromRoute] DeleteMealRequest request,
            CancellationToken cancellationToken)
        {
            var response = new DeleteMealResponse(request.CorrelationId());
            var meal = await _mealService.DeleteAsync(request.Id);
            return Ok(response);
        }
    }
}