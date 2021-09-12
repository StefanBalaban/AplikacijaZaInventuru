using ApplicationCore.Interfaces;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace PublicApi.Endpoints.NotificationRuleEndpoints
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GetById : BaseAsyncEndpoint.WithRequest<GetByIdNotificationRuleRequest>.WithResponse<GetByIdNotificationRuleResponse>
    {
        private readonly INotificationRuleService _notificationRuleService;
        private readonly IMapper _mapper;
        public GetById(INotificationRuleService notificationRuleService, IMapper mapper)
        {
            _notificationRuleService = notificationRuleService;
            _mapper = mapper;
        }

        [HttpGet("api/notificationrule/{Id}")]
        [SwaggerOperation(Summary = "GetById NotificationRule", Description = "GetById NotificationRule", OperationId = "notificationrule.getbyid", Tags = new[] { "NotificationRuleEndpoints" })]
        public override async Task<ActionResult<GetByIdNotificationRuleResponse>> HandleAsync([FromRoute] GetByIdNotificationRuleRequest request, CancellationToken cancellationToken)
        {
            var response = new GetByIdNotificationRuleResponse(request.CorrelationId());
            var notificationRule = await _notificationRuleService.GetAsync(request.Id);
            response.NotificationRule = _mapper.Map<NotificationRuleDto>(notificationRule);
            return Ok(response);
        }
    }
}

