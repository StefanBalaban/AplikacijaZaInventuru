using Ardalis.GuardClauses;

namespace ApplicationCore.Entities.UserAggregate
{
    public class UserContactInfo : BaseEntity
    {
        public string Contact { get; private set; }

        public UserContactInfo()
        {
        }
    }
}