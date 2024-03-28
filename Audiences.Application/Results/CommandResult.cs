using Audiences.Application.Validation;

namespace Audiences.Application.Results
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
