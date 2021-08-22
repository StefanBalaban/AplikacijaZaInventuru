using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications.UserSubscriptionSpecs;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using PublicApi.Util;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;

namespace PublicApi.Endpoints.UserSubscriptionEndpoints
{
    [Authorize(Roles = "Administrators", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class Create : BaseAsyncEndpoint.WithRequest<CreateUserSubscriptionRequest>.WithResponse<CreateUserSubscriptionResponse>
{
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly IMapper _mapper;
        public Create(IUserSubscriptionService userSubscriptionService, IMapper mapper)
        {
            _userSubscriptionService = userSubscriptionService;
            _mapper = mapper;
        }

        [HttpPost("api/usersubscription")]
        [SwaggerOperation(Summary = "Create UserSubscription", Description = "Create UserSubscription", OperationId = "usersubscription.create", Tags = new[] { "UserSubscriptionEndpoints" })]
        public override async Task<ActionResult<CreateUserSubscriptionResponse>> HandleAsync(CreateUserSubscriptionRequest request, CancellationToken cancellationToken)
        {
            var response = new CreateUserSubscriptionResponse(request.CorrelationId());
            var userSubscription = await _userSubscriptionService.PostAsync(new UserSubscription { UserId = request.UserId, PaymentDate = request.PaymentDate, BegginDate = request.BegginDate, EndDate = request.EndDate });
            response.UserSubscription = _mapper.Map<UserSubscriptionDto>(userSubscription);
            return response;
        }
    }

    public class CreateUserSubscriptionRequest : BaseRequest
    {
        public int UserId { get; set; }

        public DateTime PaymentDate { get; set; }

        public DateTime BegginDate { get; set; }

        public DateTime EndDate { get; set; }
    }

    public class CreateUserSubscriptionResponse : BaseResponse
    {
        public CreateUserSubscriptionResponse(Guid correlationId) : base(correlationId)
        {
        }

        public CreateUserSubscriptionResponse()
        {
        }

        public UserSubscriptionDto UserSubscription { get; set; }
    }

    [Authorize(Roles = "Administrators", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Update : BaseAsyncEndpoint.WithRequest<UpdateUserSubscriptionRequest>.WithResponse<UpdateUserSubscriptionResponse>
{
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly IMapper _mapper;
        public Update(IUserSubscriptionService userSubscriptionService, IMapper mapper)
        {
            _userSubscriptionService = userSubscriptionService;
            _mapper = mapper;
        }

        [HttpPut("api/usersubscription")]
        [SwaggerOperation(Summary = "Update UserSubscription", Description = "Update UserSubscription", OperationId = "usersubscription.update", Tags = new[] { "UserSubscriptionEndpoints" })]
        public override async Task<ActionResult<UpdateUserSubscriptionResponse>> HandleAsync(UpdateUserSubscriptionRequest request, CancellationToken cancellationToken)
        {
            var response = new UpdateUserSubscriptionResponse(request.CorrelationId());
            var userSubscription = await _userSubscriptionService.PutAsync(new UserSubscription { PaymentDate = request.PaymentDate, BegginDate = request.BegginDate, EndDate = request.EndDate });
            response.UserSubscription = _mapper.Map<UserSubscriptionDto>(userSubscription);
            return response;
        }
    }

    public class UpdateUserSubscriptionRequest : BaseRequest
    {
        public int Id { get; set; }

        public DateTime PaymentDate { get; set; }

        public DateTime BegginDate { get; set; }

        public DateTime EndDate { get; set; }
    }

    public class UpdateUserSubscriptionResponse : BaseResponse
    {
        public UpdateUserSubscriptionResponse(Guid correlationId) : base(correlationId)
        {
        }

        public UpdateUserSubscriptionResponse()
        {
        }

        public UserSubscriptionDto UserSubscription { get; set; }
    }

    [Authorize(Roles = "Administrators", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GetById : BaseAsyncEndpoint.WithRequest<GetByIdUserSubscriptionRequest>.WithResponse<GetByIdUserSubscriptionResponse>
{
private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly IMapper _mapper;
        public GetById(IUserSubscriptionService userSubscriptionService, IMapper mapper)
        {
            _userSubscriptionService = userSubscriptionService;
            _mapper = mapper;
        }

        [HttpGet("api/usersubscription/{Id}")]
        [SwaggerOperation(Summary = "GetById UserSubscription", Description = "GetById UserSubscription", OperationId = "usersubscription.getbyid", Tags = new[] { "UserSubscriptionEndpoints" })]
        public override async Task<ActionResult<GetByIdUserSubscriptionResponse>> HandleAsync([FromRoute] GetByIdUserSubscriptionRequest request, CancellationToken cancellationToken)
        {
            var response = new GetByIdUserSubscriptionResponse(request.CorrelationId());
            var userSubscription = await _userSubscriptionService.GetAsync(request.Id);
            response.UserSubscription = _mapper.Map<UserSubscriptionDto>(userSubscription);
            return Ok(response);
        }
    }

    public class GetByIdUserSubscriptionRequest : BaseRequest
    {
        public int Id { get; set; }
    }

    public class GetByIdUserSubscriptionResponse : BaseResponse
    {
        public GetByIdUserSubscriptionResponse(Guid correlationId) : base(correlationId)
        {
        }

        public GetByIdUserSubscriptionResponse()
        {
        }

        public UserSubscriptionDto UserSubscription { get; set; }
    }

    [Authorize(Roles = "Administrators", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Delete : BaseAsyncEndpoint.WithRequest<DeleteUserSubscriptionRequest>.WithResponse<DeleteUserSubscriptionResponse>
{
private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly IMapper _mapper;
        public Delete(IUserSubscriptionService userSubscriptionService, IMapper mapper)
        {
            _userSubscriptionService = userSubscriptionService;
            _mapper = mapper;
        }

        [HttpDelete("api/usersubscription/{Id}")]
        [SwaggerOperation(Summary = "Delete UserSubscription", Description = "Delete UserSubscription", OperationId = "usersubscription.delete", Tags = new[] { "UserSubscriptionEndpoints" })]
        public override async Task<ActionResult<DeleteUserSubscriptionResponse>> HandleAsync([FromRoute] DeleteUserSubscriptionRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteUserSubscriptionResponse(request.CorrelationId());
            var userSubscription = await _userSubscriptionService.DeleteAsync(request.Id);
            return Ok(response);
        }
    }

    public class DeleteUserSubscriptionRequest : BaseRequest
    {
        public int Id { get; set; }
    }

    public class DeleteUserSubscriptionResponse : BaseResponse
    {
        public DeleteUserSubscriptionResponse(Guid correlationId) : base(correlationId)
        {
        }

        public DeleteUserSubscriptionResponse()
        {
        }

        public string Status { get; set; } = "Deleted";
    }

    [Authorize(Roles = "Administrators", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ListPaged : BaseAsyncEndpoint.WithRequest<ListPagedUserSubscriptionRequest>.WithResponse<ListPagedUserSubscriptionResponse>
{
private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly IMapper _mapper;
        public ListPaged(IUserSubscriptionService userSubscriptionService, IMapper mapper)
        {
            _userSubscriptionService = userSubscriptionService;
            _mapper = mapper;
        }

        [HttpGet("api/usersubscription")]
        [SwaggerOperation(Summary = "ListPaged UserSubscription", Description = "ListPaged UserSubscription", OperationId = "usersubscription.listpaged", Tags = new[] { "UserSubscriptionEndpoints" })]
        public override async Task<ActionResult<ListPagedUserSubscriptionResponse>> HandleAsync([FromQuery] ListPagedUserSubscriptionRequest request, CancellationToken cancellationToken)
        {
            var response = new ListPagedUserSubscriptionResponse(request.CorrelationId());
            var filterSpec = new UserSubscriptionFilterSpecification(request.BegginDateGTE, request.BegginDateLTE, request.EndDateGTE, request.EndDateLTE);
            var pagedSpec = new UserSubscriptionFilterPaginatedSpecification(request.PageIndex * request.PageSize, request.PageSize, request.BegginDateGTE, request.BegginDateLTE, request.EndDateGTE, request.EndDateLTE);
            var userSubscriptions = await _userSubscriptionService.GetAsync(filterSpec, pagedSpec);
            response.UserSubscriptions.AddRange(userSubscriptions.List.Select(_mapper.Map<UserSubscriptionDto>));
            response.PageCount = userSubscriptions.List.Count;
            return Ok(response);
        }
    }

    public class ListPagedUserSubscriptionRequest : BaseRequest
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public DateTime? BegginDateGTE { get; set; }

        public DateTime? BegginDateLTE { get; set; }

        public DateTime? EndDateGTE { get; set; }

        public DateTime? EndDateLTE { get; set; }
    }

    public class ListPagedUserSubscriptionResponse : BaseResponse
    {
        public ListPagedUserSubscriptionResponse(Guid correlationId) : base(correlationId)
        {
        }

        public ListPagedUserSubscriptionResponse()
        {
        }

        public List<UserSubscriptionDto> UserSubscriptions { get; set; } = new List<UserSubscriptionDto>();
        public int PageCount { get; set; }
    }

    public class UserSubscriptionDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime PaymentDate { get; set; }

        public DateTime BegginDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
