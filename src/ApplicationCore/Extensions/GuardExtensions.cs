using ApplicationCore.Exceptions;
using ApplicationCore.Helpers;
using Ardalis.GuardClauses;

namespace ApplicationCore.Extensions
{
    public static class BasketGuards
    {

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