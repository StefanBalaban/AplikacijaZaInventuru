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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Update : BaseAsyncEndpoint.WithRequest<UpdateUserWeightEvidentionRequest>.WithResponse<UpdateUserWeightEvidentionResponse>
    {
        private readonly IUserWeightEvidentionService _userWeightEvidentionService;
        private readonly IMapper _mapper;
        public Update(IUserWeightEvidentionService userWeightEvidentionService, IMapper mapper)
        {
            _userWeightEvidentionService = userWeightEvidentionService;
            _mapper = mapper;
        }

        [HttpPut("api/userweightevidention")]
        [SwaggerOperation(Summary = "Update UserWeightEvidention", Description = "Update UserWeightEvidention", OperationId = "userweightevidention.update", Tags = new[] { "UserWeightEvidentionEndpoints" })]
        public override async Task<ActionResult<UpdateUserWeightEvidentionResponse>> HandleAsync(UpdateUserWeightEvidentionRequest request, CancellationToken cancellationToken)
        {
            var response = new UpdateUserWeightEvidentionResponse(request.CorrelationId());
            var userWeightEvidention = await _userWeightEvidentionService.PutAsync(new UserWeightEvidention { EvidentationDate = request.EvidentationDate, Weight = request.Weight });
            response.UserWeightEvidention = _mapper.Map<UserWeightEvidentionDto>(userWeightEvidention);
            return response;
        }
    }
}
