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
    public class Delete : BaseAsyncEndpoint.WithRequest<DeleteUserWeightEvidentionRequest>.WithResponse<DeleteUserWeightEvidentionResponse>
    {
        private readonly IUserWeightEvidentionService _userWeightEvidentionService;
        private readonly IMapper _mapper;
        public Delete(IUserWeightEvidentionService userWeightEvidentionService, IMapper mapper)
        {
            _userWeightEvidentionService = userWeightEvidentionService;
            _mapper = mapper;
        }

        [HttpDelete("api/userweightevidention/{Id}")]
        [SwaggerOperation(Summary = "Delete UserWeightEvidention", Description = "Delete UserWeightEvidention", OperationId = "userweightevidention.delete", Tags = new[] { "UserWeightEvidentionEndpoints" })]
        public override async Task<ActionResult<DeleteUserWeightEvidentionResponse>> HandleAsync([FromRoute] DeleteUserWeightEvidentionRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteUserWeightEvidentionResponse(request.CorrelationId());
            var userWeightEvidention = await _userWeightEvidentionService.DeleteAsync(request.Id);
            return Ok(response);
        }
    }
}
