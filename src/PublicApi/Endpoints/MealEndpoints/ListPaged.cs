using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications.MealSpecs;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PublicApi.Endpoints.MealEndpoints
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ListPaged : BaseAsyncEndpoint.WithRequest<ListPagedMealRequest>.WithResponse<ListPagedMealResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMealService _mealService;

        public ListPaged(IMealService mealService, IMapper mapper)
        {
            _mealService = mealService;
            _mapper = mapper;
        }

        [HttpGet("api/meal")]
        [SwaggerOperation(Summary = "ListPaged Meal", Description = "ListPaged Meal", OperationId = "meal.listpaged",
            Tags = new[] { "MealEndpoints" })]
        public override async Task<ActionResult<ListPagedMealResponse>> HandleAsync(
            [FromQuery] ListPagedMealRequest request, CancellationToken cancellationToken)
        {
            var response = new ListPagedMealResponse(request.CorrelationId());
            var filterSpec = new MealFilterSpecification();
            var pagedSpec =
                new MealFilterPaginatedSpecification(request.UserId ,request.PageIndex * request.PageSize, request.PageSize);
            var meals = await _mealService.GetAsync(filterSpec, pagedSpec);
            response.Meals.AddRange(meals.List.Select(_mapper.Map<MealDto>));
            response.PageCount = meals.List.Count;
            return Ok(response);
        }
    }
}