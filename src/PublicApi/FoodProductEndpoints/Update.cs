using Ardalis.ApiEndpoints;
using AutoMapper;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace PublicApi.Util.FoodProductEndpoints
{
    [Authorize(Roles = "Administrators", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Update : BaseAsyncEndpoint.WithRequest<UpdateFoodProductRequest>.WithResponse<UpdateFoodProductResponse>
    {
        private readonly IFoodProductService _foodProductService;
        private readonly IMapper _mapper;
        public Update(IFoodProductService foodProductService, IMapper mapper)
        {
            _foodProductService = foodProductService;
            _mapper = mapper;
        }

        [HttpPut("api/foodproduct")]
        [SwaggerOperation(Summary = "Update FoodProduct", Description = "Update FoodProduct", OperationId = "foodproduct.update", Tags = new[] { "FoodProductEndpoints" })]
        public override async Task<ActionResult<UpdateFoodProductResponse>> HandleAsync(UpdateFoodProductRequest request, CancellationToken cancellationToken)
        {
            var response = new UpdateFoodProductResponse(request.CorrelationId());
            var foodProduct = await _foodProductService.PutAsync(new FoodProduct { Name = request.Name, UnitOfMeasureId = request.UnitOfMeasureId, Calories = request.Calories, Protein = request.Protein, Carbohydrates = request.Carbohydrates });
            response.FoodProduct = _mapper.Map<FoodProductDto>(foodProduct);
            return response;
        }
    }
}