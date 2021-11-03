using ApplicationCore.Interfaces;
using ApplicationCore.Specifications.NotificationRuleSpecs;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PublicApi.Endpoints.NotificationRuleEndpoints
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ListPaged : BaseAsyncEndpoint.WithRequest<ListPagedNotificationRuleRequest>.WithResponse<ListPagedNotificationRuleResponse>
    {
        private readonly INotificationRuleService _notificationRuleService;
        private readonly IMapper _mapper;
        public ListPaged(INotificationRuleService notificationRuleService, IMapper mapper)
        {
            _notificationRuleService = notificationRuleService;
            _mapper = mapper;
        }

        [HttpGet("api/notificationrule")]
        [SwaggerOperation(Summary = "ListPaged NotificationRule", Description = "ListPaged NotificationRule", OperationId = "notificationrule.listpaged", Tags = new[] { "NotificationRuleEndpoints" })]
        public override async Task<ActionResult<ListPagedNotificationRuleResponse>> HandleAsync([FromQuery] ListPagedNotificationRuleRequest request, CancellationToken cancellationToken)
        {
            var response = new ListPagedNotificationRuleResponse(request.CorrelationId());
            var filterSpec = new NotificationRuleFilterSpecification(request.FoodProductId);
            var pagedSpec = new NotificationRuleFilterPaginatedSpecification(request.UserId ,request.PageIndex * request.PageSize, request.PageSize, request.FoodProductId);
            var notificationRules = await _notificationRuleService.GetAsync(filterSpec, pagedSpec);
            response.NotificationRules.AddRange(notificationRules.List.Select(_mapper.Map<NotificationRuleDto>));
            response.PageCount = notificationRules.List.Count;
            return Ok(response);
        }
    }
}

