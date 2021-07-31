using ApplicationCore.Exceptions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.Helpers
{
    internal class ModelStateValidationHelper
    {
        public void ValidateModelState(object model, string name)
        {
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, context, results);
            if (!isValid)
            {

                var message = new StringBuilder();
                results.ForEach(x => message.Append(x.ErrorMessage + " "));
                throw new ModelStateInvalidException(message.ToString(), name);
            }
        }
    }
}