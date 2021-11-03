using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications.FoodStockSpecs;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PublicApi.Endpoints.FoodStockEndpoints
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ListPaged : BaseAsyncEndpoint.WithRequest<ListPagedFoodStockRequest>.WithResponse<
        ListPagedFoodStockResponse>
    {
        private readonly IFoodStockService _foodStockService;
        private readonly IMapper _mapper;

        public ListPaged(IFoodStockService foodStockService, IMapper mapper)
        {
            _foodStockService = foodStockService;
            _mapper = mapper;
        }

        [HttpGet("api/foodstock")]
        [SwaggerOperation(Summary = "ListPaged FoodStock", Description = "ListPaged FoodStock",
            OperationId = "foodstock.listpaged", Tags = new[] { "FoodStockEndpoints" })]
        public override async Task<ActionResult<ListPagedFoodStockResponse>> HandleAsync(
            [FromQuery] ListPagedFoodStockRequest request, CancellationToken cancellationToken)
        {
            var response = new ListPagedFoodStockResponse(request.CorrelationId());
            var filterSpec = new FoodStockFilterSpecification(request.FoodProductId);
            var pagedSpec = new FoodStockFilterPaginatedSpecification(request.UserId ,request.PageIndex * request.PageSize,
                request.PageSize, request.FoodProductId);
            var foodStocks = await _foodStockService.GetAsync(filterSpec, pagedSpec);
            response.FoodStocks.AddRange(foodStocks.List.Select(_mapper.Map<FoodStockDto>));
            response.PageCount = foodStocks.List.Count;
            return Ok(response);
        }
    }
}