using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PublicApi.Endpoints.FoodStockEndpoints
{
    [Authorize(Roles = "Administrators", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GetById : BaseAsyncEndpoint.WithRequest<GetByIdFoodStockRequest>.WithResponse<GetByIdFoodStockResponse>
    {
        private readonly IFoodStockService _foodStockService;
        private readonly IMapper _mapper;

        public GetById(IFoodStockService foodStockService, IMapper mapper)
        {
            _foodStockService = foodStockService;
            _mapper = mapper;
        }

        [HttpGet("api/foodstock/{Id}")]
        [SwaggerOperation(Summary = "GetById FoodStock", Description = "GetById FoodStock",
            OperationId = "foodstock.getbyid", Tags = new[] { "FoodStockEndpoints" })]
        public override async Task<ActionResult<GetByIdFoodStockResponse>> HandleAsync(
            [FromRoute] GetByIdFoodStockRequest request, CancellationToken cancellationToken)
        {
            var response = new GetByIdFoodStockResponse(request.CorrelationId());
            var foodStock = await _foodStockService.GetAsync(request.Id);
            response.FoodStock = _mapper.Map<FoodStockDto>(foodStock);
            return Ok(response);
        }
    }
}