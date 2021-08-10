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
    public class Delete : BaseAsyncEndpoint.WithRequest<DeleteFoodStockRequest>.WithResponse<DeleteFoodStockResponse>
    {
        private readonly IFoodStockService _foodStockService;
        private readonly IMapper _mapper;

        public Delete(IFoodStockService foodStockService, IMapper mapper)
        {
            _foodStockService = foodStockService;
            _mapper = mapper;
        }

        [HttpDelete("api/foodstock/{Id}")]
        [SwaggerOperation(Summary = "Delete FoodStock", Description = "Delete FoodStock",
            OperationId = "foodstock.delete", Tags = new[] { "FoodStockEndpoints" })]
        public override async Task<ActionResult<DeleteFoodStockResponse>> HandleAsync(
            [FromRoute] DeleteFoodStockRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteFoodStockResponse(request.CorrelationId());
            var foodStock = await _foodStockService.DeleteAsync(request.Id);
            return Ok(response);
        }
    }
}