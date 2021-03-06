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
    public class Create : BaseAsyncEndpoint.WithRequest<CreateFoodProductRequest>.WithResponse<
        CreateFoodProductResponse>
    {
        private readonly IFoodProductService _foodProductService;
        private readonly IMapper _mapper;

        public Create(IFoodProductService foodProductService, IMapper mapper)
        {
            _foodProductService = foodProductService;
            _mapper = mapper;
        }

        [HttpPost("api/foodproduct")]
        [SwaggerOperation(Summary = "Create FoodProduct", Description = "Create FoodProduct",
            OperationId = "foodproduct.create", Tags = new[] { "FoodProductEndpoints" })]
        public override async Task<ActionResult<CreateFoodProductResponse>> HandleAsync(
            CreateFoodProductRequest request, CancellationToken cancellationToken)
        {
            var response = new CreateFoodProductResponse(request.CorrelationId());
            var foodProduct = await _foodProductService.PostAsync(new FoodProduct
            {
                UserId = request.UserId,
                Name = request.Name, UnitOfMeasureId = request.UnitOfMeasureId, Calories = request.Calories,
                Protein = request.Protein, Carbohydrates = request.Carbohydrates, Fats = request.Fats
            });
            response.FoodProduct = _mapper.Map<FoodProductDto>(foodProduct);
            return response;
        }
    }
}