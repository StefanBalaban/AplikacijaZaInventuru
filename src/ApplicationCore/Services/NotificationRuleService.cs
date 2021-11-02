using ApplicationCore.Entities.NotificationAggregate;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Extensions;
using ApplicationCore.Specifications.NotificationRuleSpecs;

namespace ApplicationCore.Services
{
    public class NotificationRuleService : INotificationRuleService
    {
        private readonly IAsyncRepository<NotificationRule> _notificationRuleRepository;
        private readonly IAppLogger<NotificationRuleService> _logger;
        public NotificationRuleService(IAsyncRepository<NotificationRule> notificationRuleRepository, IAppLogger<NotificationRuleService> logger)
        {
            _notificationRuleRepository = notificationRuleRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<NotificationRule>> GetAsync()
        {
            return await _notificationRuleRepository.ListAllAsync();
        }

        public async Task<NotificationRule> GetAsync(int id)
        {
            var spec = new NotificationRuleByIdFilterSpecification(id);
            var notificationRule = await _notificationRuleRepository.FirstOrDefaultAsync(spec);
            Guard.Against.EntityNotFound(notificationRule, nameof(NotificationRule));
            return notificationRule;
        }

        public async Task<ListEntity<NotificationRule>> GetAsync(Specification<NotificationRule> filterSpec, Specification<NotificationRule> pagedSpec)
        {
            return new ListEntity<NotificationRule> { List = await _notificationRuleRepository.ListAsync(pagedSpec), Count = await _notificationRuleRepository.CountAsync(filterSpec) };
        }

        public async Task<NotificationRule> PostAsync(NotificationRule t)
        {
            Guard.Against.ModelStateIsInvalid(t, nameof(NotificationRule));
            return await _notificationRuleRepository.AddAsync(new NotificationRule { FoodProductId = t.FoodProductId, NotificationRuleUserContactInfos = t.NotificationRuleUserContactInfos });
        }

        public async Task<NotificationRule> PutAsync(NotificationRule t)
        {
            Guard.Against.ModelStateIsInvalid(t, nameof(NotificationRule));
            var spec = new NotificationRuleByIdFilterSpecification(t.Id);
            var notificationRule = await _notificationRuleRepository.FirstOrDefaultAsync(spec);
            Guard.Against.EntityNotFound(notificationRule, nameof(NotificationRule));
            notificationRule.NotificationRuleUserContactInfos = t.NotificationRuleUserContactInfos;
            await _notificationRuleRepository.UpdateAsync(notificationRule);
            return notificationRule;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var notificationRule = await _notificationRuleRepository.GetByIdAsync(id);
            Guard.Against.EntityNotFound(notificationRule, nameof(NotificationRule));
            await _notificationRuleRepository.DeleteAsync(notificationRule);
            return true;
        }
    }
}

