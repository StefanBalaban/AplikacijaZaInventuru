using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PublicApi.Endpoints.FoodStockEndpoints
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Update : BaseAsyncEndpoint.WithRequest<UpdateFoodStockRequest>.WithResponse<UpdateFoodStockResponse>
    {
        private readonly IFoodStockService _foodStockService;
        private readonly IMapper _mapper;

        public Update(IFoodStockService foodStockService, IMapper mapper)
        {
            _foodStockService = foodStockService;
            _mapper = mapper;
        }

        [HttpPut("api/foodstock")]
        [SwaggerOperation(Summary = "Update FoodStock", Description = "Update FoodStock",
            OperationId = "foodstock.update", Tags = new[] { "FoodStockEndpoints" })]
        public override async Task<ActionResult<UpdateFoodStockResponse>> HandleAsync(UpdateFoodStockRequest request,
            CancellationToken cancellationToken)
        {
            var response = new UpdateFoodStockResponse(request.CorrelationId());
            var foodStock = await _foodStockService.PutAsync(new FoodStock
            {
                Amount = request.Amount, UpperAmount = request.UpperAmount, LowerAmount = request.LowerAmount,
                DateOfPurchase = request.DateOfPurchase, BestUseByDate = request.BestUseByDate
            });
            response.FoodStock = _mapper.Map<FoodStockDto>(foodStock);
            return response;
        }
    }
}