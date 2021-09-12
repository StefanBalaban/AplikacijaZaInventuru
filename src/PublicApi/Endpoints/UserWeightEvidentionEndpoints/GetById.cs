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
    public class GetById : BaseAsyncEndpoint.WithRequest<GetByIdUserWeightEvidentionRequest>.WithResponse<GetByIdUserWeightEvidentionResponse>
    {
        private readonly IUserWeightEvidentionService _userWeightEvidentionService;
        private readonly IMapper _mapper;
        public GetById(IUserWeightEvidentionService userWeightEvidentionService, IMapper mapper)
        {
            _userWeightEvidentionService = userWeightEvidentionService;
            _mapper = mapper;
        }

        [HttpGet("api/userweightevidention/{Id}")]
        [SwaggerOperation(Summary = "GetById UserWeightEvidention", Description = "GetById UserWeightEvidention", OperationId = "userweightevidention.getbyid", Tags = new[] { "UserWeightEvidentionEndpoints" })]
        public override async Task<ActionResult<GetByIdUserWeightEvidentionResponse>> HandleAsync([FromRoute] GetByIdUserWeightEvidentionRequest request, CancellationToken cancellationToken)
        {
            var response = new GetByIdUserWeightEvidentionResponse(request.CorrelationId());
            var userWeightEvidention = await _userWeightEvidentionService.GetAsync(request.Id);
            response.UserWeightEvidention = _mapper.Map<UserWeightEvidentionDto>(userWeightEvidention);
            return Ok(response);
        }
    }
}
