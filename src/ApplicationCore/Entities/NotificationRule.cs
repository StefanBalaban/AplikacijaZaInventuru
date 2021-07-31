using ApplicationCore.Entities.UserAggregate;
using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationCore.Entities
{
    public class NotificationRule : BaseEntity, IAggregateRoot
    {
        private readonly List<UserContactInfo> _userContactInfos = new List<UserContactInfo>();
        public IReadOnlyList<UserContactInfo> UserContactInfos => _userContactInfos.AsReadOnly();
        public FoodProduct FoodProduct { get; private set; }

        public NotificationRule()
        {
        }

        public NotificationRule(List<UserContactInfo> userContactInfos, FoodProduct foodProduct)
        {
            Guard.Against.Null(userContactInfos, nameof(userContactInfos));
            Guard.Against.Null(foodProduct, nameof(foodProduct));

            _userContactInfos = userContactInfos;
            FoodProduct = foodProduct;
        }

        public void EditFoodProduct(FoodProduct foodProduct)
        {
            Guard.Against.Null(foodProduct, nameof(foodProduct));

            FoodProduct = foodProduct;
        }

        public void AddUserContactInfos(List<UserContactInfo> userContactInfos)
        {
            Guard.Against.Null(userContactInfos, nameof(userContactInfos));

            _userContactInfos.AddRange(userContactInfos);
        }

        public void RemoveUserContactInfos(List<UserContactInfo> userContactInfos)
        {
            Guard.Against.Null(userContactInfos, nameof(userContactInfos));

            userContactInfos.ForEach(userContactInfo =>
            {
                var userContactInfoToRemove = _userContactInfos.SingleOrDefault(y => y.Id == userContactInfo.Id);
                if (userContactInfoToRemove != null) _userContactInfos.Remove(userContactInfo);
            });
        }
    }
}