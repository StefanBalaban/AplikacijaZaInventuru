using ApplicationCore.Entities;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using Ardalis.Specification;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class UserSubscriptionService : IUserSubscriptionService
    {
        private readonly IAsyncRepository<UserSubscription> _userSubscriptionRepository;
        private readonly IAppLogger<UserSubscriptionService> _logger;
        public UserSubscriptionService(IAsyncRepository<UserSubscription> userSubscriptionRepository, IAppLogger<UserSubscriptionService> logger)
        {
            _userSubscriptionRepository = userSubscriptionRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<UserSubscription>> GetAsync()
        {
            return await _userSubscriptionRepository.ListAllAsync();
        }

        public async Task<UserSubscription> GetAsync(int id)
        {
            var userSubscription = await _userSubscriptionRepository.GetByIdAsync(id);
            Guard.Against.EntityNotFound(userSubscription, nameof(UserSubscription));
            return userSubscription;
        }

        public async Task<ListEntity<UserSubscription>> GetAsync(Specification<UserSubscription> filterSpec, Specification<UserSubscription> pagedSpec)
        {
            return new ListEntity<UserSubscription> { List = await _userSubscriptionRepository.ListAsync(pagedSpec), Count = await _userSubscriptionRepository.CountAsync(filterSpec) };
        }

        public async Task<UserSubscription> PostAsync(UserSubscription t)
        {
            Guard.Against.ModelStateIsInvalid(t, nameof(UserSubscription));
            return await _userSubscriptionRepository.AddAsync(new UserSubscription { UserId = t.UserId, PaymentDate = t.PaymentDate, BegginDate = t.BegginDate, EndDate = t.EndDate });
        }

        public async Task<UserSubscription> PutAsync(UserSubscription t)
        {
            Guard.Against.ModelStateIsInvalid(t, nameof(UserSubscription));
            var userSubscription = await _userSubscriptionRepository.GetByIdAsync(t.Id);
            Guard.Against.EntityNotFound(userSubscription, nameof(UserSubscription));
            userSubscription.PaymentDate = t.PaymentDate;
            userSubscription.BegginDate = t.BegginDate;
            userSubscription.EndDate = t.EndDate;
            await _userSubscriptionRepository.UpdateAsync(userSubscription);
            return userSubscription;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var userSubscription = await _userSubscriptionRepository.GetByIdAsync(id);
            Guard.Against.EntityNotFound(userSubscription, nameof(UserSubscription));
            await _userSubscriptionRepository.DeleteAsync(userSubscription);
            return true;
        }
    }
}
