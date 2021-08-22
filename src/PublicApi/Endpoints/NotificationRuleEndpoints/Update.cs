using ApplicationCore.Entities.NotificationAggregate;
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
    public class Update : BaseAsyncEndpoint.WithRequest<UpdateNotificationRuleRequest>.WithResponse<UpdateNotificationRuleResponse>
    {
        private readonly INotificationRuleService _notificationRuleService;
        private readonly IMapper _mapper;
        public Update(INotificationRuleService notificationRuleService, IMapper mapper)
        {
            _notificationRuleService = notificationRuleService;
            _mapper = mapper;
        }

        [HttpPut("api/notificationrule")]
        [SwaggerOperation(Summary = "Update NotificationRule", Description = "Update NotificationRule", OperationId = "notificationrule.update", Tags = new[] { "NotificationRuleEndpoints" })]
        public override async Task<ActionResult<UpdateNotificationRuleResponse>> HandleAsync(UpdateNotificationRuleRequest request, CancellationToken cancellationToken)
        {
            var response = new UpdateNotificationRuleResponse(request.CorrelationId());
            var notificationRule = await _notificationRuleService.PutAsync(new NotificationRule { NotificationRuleUserContactInfos = request.NotificationRuleUserContactInfos });
            response.NotificationRule = _mapper.Map<NotificationRuleDto>(notificationRule);
            return response;
        }
    }
}

