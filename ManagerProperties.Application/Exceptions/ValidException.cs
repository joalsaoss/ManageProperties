using FluentValidation.Results;

namespace ManagerProperties.Application.Exceptions
{
    public class ValidException: Exception
    {
        public List<string> ValidationErrors { get; set; } = [];

        public ValidException(ValidationResult validationResult)
        {
            foreach (var validationError in validationResult.Errors)
            {
                ValidationErrors.Add(validationError.ErrorMessage);
            }
        }
    }
}
