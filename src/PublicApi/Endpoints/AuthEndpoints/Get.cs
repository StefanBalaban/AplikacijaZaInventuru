using ApplicationCore.Interfaces;
using Ardalis.ApiEndpoints;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using PublicApi.Util.AuthEndpoints;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PublicApi.Endpoints.AuthEndpoints
{
    [Authorize]
    public class Get : BaseAsyncEndpoint.WithoutRequest.WithResponse<string>
    {

        public Get()
        {

        }

        [HttpGet("api/identity")]
        [SwaggerOperation(
            Summary = "Gets claims",
            Description = "Get claims",
            OperationId = "auth.identity",
            Tags = new[] { "AuthEndpoints" })
        ]
        public override async Task<ActionResult<string>> HandleAsync(
            CancellationToken cancellationToken)
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}
