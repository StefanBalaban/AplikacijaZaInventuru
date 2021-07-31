using ApplicationCore.Extensions;
using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationCore.Entities.UserAggregate
{
    public class User : BaseEntity, IAggregateRoot
    {
        private readonly List<UserContactInfo> _userContactInfos = new List<UserContactInfo>();
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public IReadOnlyList<UserContactInfo> UserContactInfos => _userContactInfos.AsReadOnly();

        public User()
        {
        }
    }
}