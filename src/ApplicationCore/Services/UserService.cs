using ApplicationCore.Entities;
using ApplicationCore.Entities.UserAggregate;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications.UserSpecs;
using Ardalis.GuardClauses;
using Ardalis.Specification;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class UserService : IUserService
    {
        private readonly IAsyncRepository<User> _userRepository;
        private readonly IAppLogger<UserService> _logger;
        public UserService(IAsyncRepository<User> userRepository, IAppLogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            return await _userRepository.ListAllAsync();
        }

        public async Task<User> GetAsync(int id)
        {
            var spec = new UserByIdFilterSpecification(id);
            var user = await _userRepository.FirstOrDefaultAsync(spec);
            Guard.Against.EntityNotFound(user, nameof(User));
            return user;
        }

        public async Task<ListEntity<User>> GetAsync(Specification<User> filterSpec, Specification<User> pagedSpec)
        {
            return new ListEntity<User> { List = await _userRepository.ListAsync(pagedSpec), Count = await _userRepository.CountAsync(filterSpec) };
        }

        public async Task<User> PostAsync(User t)
        {
            Guard.Against.ModelStateIsInvalid(t, nameof(User));
            return await _userRepository.AddAsync(new User { FirstName = t.FirstName, LastName = t.LastName, UserContactInfos = new List<UserContactInfo> { new UserContactInfo { Contact = t.FirstName } } });
        }

        public async Task<User> PutAsync(User t)
        {
            Guard.Against.ModelStateIsInvalid(t, nameof(User));
            var user = await _userRepository.GetByIdAsync(t.Id);
            Guard.Against.EntityNotFound(user, nameof(User));
            user.FirstName = t.FirstName;
            user.LastName = t.LastName;
            user.UserContactInfos = t.UserContactInfos;
            await _userRepository.UpdateAsync(user);
            return user;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            Guard.Against.EntityNotFound(user, nameof(User));
            await _userRepository.DeleteAsync(user);
            return true;
        }
    }
}

