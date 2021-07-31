using Ardalis.ApiEndpoints;
using AutoMapper;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications.FoodProductSpecs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PublicApi.Util.FoodProductEndpoints
{

    public class ListPaged : BaseAsyncEndpoint.WithRequest<ListPagedFoodProductRequest>.WithResponse<ListPagedFoodProductResponse>
    {
        private readonly IFoodProductService _foodProductService;
        private readonly IMapper _mapper;
        public ListPaged(IFoodProductService foodProductService, IMapper mapper)
        {
            _foodProductService = foodProductService; 
            _mapper = mapper;
        }

        [HttpGet("api/foodproduct")]
        [SwaggerOperation(Summary = "ListPaged FoodProduct", Description = "ListPaged FoodProduct", OperationId = "foodproduct.listpaged", Tags = new[] { "FoodProductEndpoints" })]
        public override async Task<ActionResult<ListPagedFoodProductResponse>> HandleAsync([FromQuery] ListPagedFoodProductRequest request, CancellationToken cancellationToken)
        {
            var response = new ListPagedFoodProductResponse(request.CorrelationId());
            var filterSpec = new FoodProductFilterSpecification(request.UnitOfMeasureId, request.CaloriesGTE, request.CaloriesLTE, request.Protein);
            var pagedSpec = new FoodProductFilterPaginatedSpecification(request.PageIndex * request.PageSize, request.PageSize, request.UnitOfMeasureId, request.CaloriesGTE, request.CaloriesLTE, request.Protein);
            var foodProducts = await _foodProductService.GetAsync(filterSpec, pagedSpec);
            response.FoodProducts.AddRange(foodProducts.List.Select(_mapper.Map<FoodProductDto>));
            response.PageCount = foodProducts.List.Count(); 
            return Ok(response);
        }
    }
}