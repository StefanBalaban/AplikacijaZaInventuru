using Ardalis.ApiEndpoints;
using AutoMapper;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace PublicApi.Util.FoodProductEndpoints
{

    public class GetById : BaseAsyncEndpoint.WithRequest<GetByIdFoodProductRequest>.WithResponse<GetByIdFoodProductResponse>
    {
        private readonly IFoodProductService _foodProductService;
        private readonly IMapper _mapper;
        public GetById(IFoodProductService foodProductService, IMapper mapper)
        {
            _foodProductService = foodProductService;
            _mapper = mapper;
        }

        [HttpGet("api/foodproduct/{FoodProductId}")] // TODO change this
        [SwaggerOperation(Summary = "GetById FoodProduct", Description = "GetById FoodProduct", OperationId = "foodproduct.getbyid", Tags = new[] { "FoodProductEndpoints" })]
        public override async Task<ActionResult<GetByIdFoodProductResponse>> HandleAsync([FromRoute] GetByIdFoodProductRequest request, CancellationToken cancellationToken)
        {
            var response = new GetByIdFoodProductResponse(request.CorrelationId());
            var foodProduct = await _foodProductService.GetAsync(request.FoodProductId);
            response.FoodProduct = _mapper.Map<FoodProductDto>(foodProduct);
            return Ok(response);
        }
    }
}