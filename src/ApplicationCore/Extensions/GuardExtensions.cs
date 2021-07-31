using ApplicationCore.Entities.UserAggregate;
using ApplicationCore.Exceptions;
using ApplicationCore.Helpers;
using Ardalis.GuardClauses;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationCore.Extensions
{
    public static class BasketGuards
    {

        public static void DuplicateContactInfo(this IGuardClause guardClause, string contact, IReadOnlyCollection<UserContactInfo> userContactInfos)
        {
            if (userContactInfos.Any(x => x.Contact == contact))
                throw new DuplicateContactInfoException();
        }
        public static void EntityNotFound(this IGuardClause guardClause, object entity, string name)
        {
            if (entity == null)
                throw new EntityNotFoundException(name);
        }

        public static void ModelStateIsInvalid(this IGuardClause guardClause, object model, string name)
        {
            new ModelStateValidationHelper().ValidateModelState(model, name);
        }

    }
}