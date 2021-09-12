using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PublicApi.Endpoints.FoodProductEndpoints
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GetById : BaseAsyncEndpoint.WithRequest<GetByIdFoodProductRequest>.WithResponse<
        GetByIdFoodProductResponse>
    {
        private readonly IFoodProductService _foodProductService;
        private readonly IMapper _mapper;

        public GetById(IFoodProductService foodProductService, IMapper mapper)
        {
            _foodProductService = foodProductService;
            _mapper = mapper;
        }

        [HttpGet("api/foodproduct/{Id}")]
        [SwaggerOperation(Summary = "GetById FoodProduct", Description = "GetById FoodProduct",
            OperationId = "foodproduct.getbyid", Tags = new[] { "FoodProductEndpoints" })]
        public override async Task<ActionResult<GetByIdFoodProductResponse>> HandleAsync(
            [FromRoute] GetByIdFoodProductRequest request, CancellationToken cancellationToken)
        {
            var response = new GetByIdFoodProductResponse(request.CorrelationId());
            var foodProduct = await _foodProductService.GetAsync(request.Id);
            response.FoodProduct = _mapper.Map<FoodProductDto>(foodProduct);
            return Ok(response);
        }
    }
}