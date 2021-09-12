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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Create : BaseAsyncEndpoint.WithRequest<CreateNotificationRuleRequest>.WithResponse<CreateNotificationRuleResponse>
    {
        private readonly INotificationRuleService _notificationRuleService;
        private readonly IMapper _mapper;
        public Create(INotificationRuleService notificationRuleService, IMapper mapper)
        {
            _notificationRuleService = notificationRuleService;
            _mapper = mapper;
        }

        [HttpPost("api/notificationrule")]
        [SwaggerOperation(Summary = "Create NotificationRule", Description = "Create NotificationRule", OperationId = "notificationrule.create", Tags = new[] { "NotificationRuleEndpoints" })]
        public override async Task<ActionResult<CreateNotificationRuleResponse>> HandleAsync(CreateNotificationRuleRequest request, CancellationToken cancellationToken)
        {
            var response = new CreateNotificationRuleResponse(request.CorrelationId());
            var notificationRule = await _notificationRuleService.PostAsync(new NotificationRule { FoodProductId = request.FoodProductId, NotificationRuleUserContactInfos = request.NotificationRuleUserContactInfos });
            response.NotificationRule = _mapper.Map<NotificationRuleDto>(notificationRule);
            return response;
        }
    }
}

