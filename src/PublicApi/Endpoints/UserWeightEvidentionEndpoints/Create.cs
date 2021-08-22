using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace PublicApi.Endpoints.UserWeightEvidentionEndpoints
{
    [Authorize(Roles = "Administrators", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Create : BaseAsyncEndpoint.WithRequest<CreateUserWeightEvidentionRequest>.WithResponse<CreateUserWeightEvidentionResponse>
    {
        private readonly IUserWeightEvidentionService _userWeightEvidentionService;
        private readonly IMapper _mapper;
        public Create(IUserWeightEvidentionService userWeightEvidentionService, IMapper mapper)
        {
            _userWeightEvidentionService = userWeightEvidentionService;
            _mapper = mapper;
        }

        [HttpPost("api/userweightevidention")]
        [SwaggerOperation(Summary = "Create UserWeightEvidention", Description = "Create UserWeightEvidention", OperationId = "userweightevidention.create", Tags = new[] { "UserWeightEvidentionEndpoints" })]
        public override async Task<ActionResult<CreateUserWeightEvidentionResponse>> HandleAsync(CreateUserWeightEvidentionRequest request, CancellationToken cancellationToken)
        {
            var response = new CreateUserWeightEvidentionResponse(request.CorrelationId());
            var userWeightEvidention = await _userWeightEvidentionService.PostAsync(new UserWeightEvidention { UserId = request.UserId, EvidentationDate = request.EvidentationDate, Weight = request.Weight });
            response.UserWeightEvidention = _mapper.Map<UserWeightEvidentionDto>(userWeightEvidention);
            return response;
        }
    }
}
