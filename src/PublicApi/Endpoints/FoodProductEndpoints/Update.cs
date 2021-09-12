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

namespace PublicApi.Endpoints.FoodProductEndpoints
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Update : BaseAsyncEndpoint.WithRequest<UpdateFoodProductRequest>.WithResponse<
        UpdateFoodProductResponse>
    {
        private readonly IFoodProductService _foodProductService;
        private readonly IMapper _mapper;

        public Update(IFoodProductService foodProductService, IMapper mapper)
        {
            _foodProductService = foodProductService;
            _mapper = mapper;
        }

        [HttpPut("api/foodproduct")]
        [SwaggerOperation(Summary = "Update FoodProduct", Description = "Update FoodProduct",
            OperationId = "foodproduct.update", Tags = new[] { "FoodProductEndpoints" })]
        public override async Task<ActionResult<UpdateFoodProductResponse>> HandleAsync(
            UpdateFoodProductRequest request, CancellationToken cancellationToken)
        {
            var response = new UpdateFoodProductResponse(request.CorrelationId());
            var foodProduct = await _foodProductService.PutAsync(new FoodProduct
            {
                Id = request.Id,
                Name = request.Name, UnitOfMeasureId = request.UnitOfMeasureId, Calories = request.Calories,
                Protein = request.Protein, Carbohydrates = request.Carbohydrates, Fats = request.Fats
            });
            response.FoodProduct = _mapper.Map<FoodProductDto>(foodProduct);
            return response;
        }
    }
}