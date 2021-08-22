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
    [Authorize(Roles = "Administrators", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Delete : BaseAsyncEndpoint.WithRequest<DeleteNotificationRuleRequest>.WithResponse<DeleteNotificationRuleResponse>
    {
        private readonly INotificationRuleService _notificationRuleService;
        private readonly IMapper _mapper;
        public Delete(INotificationRuleService notificationRuleService, IMapper mapper)
        {
            _notificationRuleService = notificationRuleService;
            _mapper = mapper;
        }

        [HttpDelete("api/notificationrule/{Id}")]
        [SwaggerOperation(Summary = "Delete NotificationRule", Description = "Delete NotificationRule", OperationId = "notificationrule.delete", Tags = new[] { "NotificationRuleEndpoints" })]
        public override async Task<ActionResult<DeleteNotificationRuleResponse>> HandleAsync([FromRoute] DeleteNotificationRuleRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteNotificationRuleResponse(request.CorrelationId());
            var notificationRule = await _notificationRuleService.DeleteAsync(request.Id);
            return Ok(response);
        }
    }
}

