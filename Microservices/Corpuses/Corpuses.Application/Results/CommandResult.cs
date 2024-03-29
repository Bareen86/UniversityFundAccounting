using Corpuses.Application.Validation;

namespace Corpuses.Application.Results
{
    public class CommandResult
    {
        public ValidationResult ValidationResult { get; set; }

        public CommandResult( ValidationResult validationResult )
        {
            ValidationResult = validationResult;
        }
    }
}
