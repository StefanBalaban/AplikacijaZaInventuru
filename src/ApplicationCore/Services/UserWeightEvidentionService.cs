using ApplicationCore.Entities;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using Ardalis.Specification;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class UserWeightEvidentionService : IUserWeightEvidentionService
    {
        private readonly IAsyncRepository<UserWeightEvidention> _userWeightEvidentionRepository;
        private readonly IAppLogger<UserWeightEvidentionService> _logger;
        public UserWeightEvidentionService(IAsyncRepository<UserWeightEvidention> userWeightEvidentionRepository, IAppLogger<UserWeightEvidentionService> logger)
        {
            _userWeightEvidentionRepository = userWeightEvidentionRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<UserWeightEvidention>> GetAsync()
        {
            return await _userWeightEvidentionRepository.ListAllAsync();
        }

        public async Task<UserWeightEvidention> GetAsync(int id)
        {
            var userWeightEvidention = await _userWeightEvidentionRepository.GetByIdAsync(id);
            Guard.Against.EntityNotFound(userWeightEvidention, nameof(UserWeightEvidention));
            return userWeightEvidention;
        }

        public async Task<ListEntity<UserWeightEvidention>> GetAsync(Specification<UserWeightEvidention> filterSpec, Specification<UserWeightEvidention> pagedSpec)
        {
            return new ListEntity<UserWeightEvidention> { List = await _userWeightEvidentionRepository.ListAsync(pagedSpec), Count = await _userWeightEvidentionRepository.CountAsync(filterSpec) };
        }

        public async Task<UserWeightEvidention> PostAsync(UserWeightEvidention t)
        {
            Guard.Against.ModelStateIsInvalid(t, nameof(UserWeightEvidention));
            return await _userWeightEvidentionRepository.AddAsync(new UserWeightEvidention { UserId = t.UserId, EvidentationDate = t.EvidentationDate, Weight = t.Weight });
        }

        public async Task<UserWeightEvidention> PutAsync(UserWeightEvidention t)
        {
            Guard.Against.ModelStateIsInvalid(t, nameof(UserWeightEvidention));
            var userWeightEvidention = await _userWeightEvidentionRepository.GetByIdAsync(t.Id);
            Guard.Against.EntityNotFound(userWeightEvidention, nameof(UserWeightEvidention));
            userWeightEvidention.EvidentationDate = t.EvidentationDate;
            userWeightEvidention.Weight = t.Weight;
            await _userWeightEvidentionRepository.UpdateAsync(userWeightEvidention);
            return userWeightEvidention;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var userWeightEvidention = await _userWeightEvidentionRepository.GetByIdAsync(id);
            Guard.Against.EntityNotFound(userWeightEvidention, nameof(UserWeightEvidention));
            await _userWeightEvidentionRepository.DeleteAsync(userWeightEvidention);
            return true;
        }
    }
}
