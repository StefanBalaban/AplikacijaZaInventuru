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
    public class Create : BaseAsyncEndpoint.WithRequest<CreateFoodStockRequest>.WithResponse<CreateFoodStockResponse>
    {
        private readonly IFoodStockService _foodStockService;
        private readonly IMapper _mapper;

        public Create(IFoodStockService foodStockService, IMapper mapper)
        {
            _foodStockService = foodStockService;
            _mapper = mapper;
        }

        [HttpPost("api/foodstock")]
        [SwaggerOperation(Summary = "Create FoodStock", Description = "Create FoodStock",
            OperationId = "foodstock.create", Tags = new[] { "FoodStockEndpoints" })]
        public override async Task<ActionResult<CreateFoodStockResponse>> HandleAsync(CreateFoodStockRequest request,
            CancellationToken cancellationToken)
        {
            var response = new CreateFoodStockResponse(request.CorrelationId());
            var foodStock = await _foodStockService.PostAsync(new FoodStock
            {
                UserId = request.UserId,
                FoodProductId = request.FoodProductId, Amount = request.Amount, UpperAmount = request.UpperAmount,
                LowerAmount = request.LowerAmount, DateOfPurchase = request.DateOfPurchase,
                BestUseByDate = request.BestUseByDate
            });
            response.FoodStock = _mapper.Map<FoodStockDto>(foodStock);
            return response;
        }
    }
}