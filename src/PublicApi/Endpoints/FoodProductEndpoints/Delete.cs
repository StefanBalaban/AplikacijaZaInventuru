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
    [Authorize(Roles = "Administrators", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Delete : BaseAsyncEndpoint.WithRequest<DeleteFoodProductRequest>.WithResponse<
        DeleteFoodProductResponse>
    {
        private readonly IFoodProductService _foodProductService;
        private readonly IMapper _mapper;

        public Delete(IFoodProductService foodProductService, IMapper mapper)
        {
            _foodProductService = foodProductService;
            _mapper = mapper;
        }

        [HttpDelete("api/foodproduct/{Id}")]
        [SwaggerOperation(Summary = "Delete FoodProduct", Description = "Delete FoodProduct",
            OperationId = "foodproduct.delete", Tags = new[] { "FoodProductEndpoints" })]
        public override async Task<ActionResult<DeleteFoodProductResponse>> HandleAsync(
            [FromRoute] DeleteFoodProductRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteFoodProductResponse(request.CorrelationId());
            var foodProduct = await _foodProductService.DeleteAsync(request.Id);
            return Ok(response);
        }
    }
}