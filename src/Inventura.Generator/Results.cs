// ApplicationCore\Interfaces\IUserSubscriptionService.cs
using ApplicationCore.Entities;

// ApplicationCore\Services\UserSubscriptionService.cs
using Ardalis.Specification;
using ApplicationCore.Interfaces;
using ApplicationCore.Extensions;
using ApplicationCore.Entities;

// ApplicationCore\Specifications\UserSubscription\UserSubscriptionFilterPaginatedSpecification.cs
using Ardalis.Specification;
using Entities;

// Infrastructure\Data\Context.cs
public DbSet<UserSubscription> UserSubscription { get; set; }
// PublicApi\Endpoints\UserSubscriptionEndpoints\Create.cs
//Mapping
//Dependency Injection
