using DotNetApi.Model.Domain.Validation;

namespace DotNetApi.Model.Domain.Exceptions
{
    public class ValidationException : Exception
    {
        public IEnumerable<ValidationError> Errors { get; } = Enumerable.Empty<ValidationError>();

        public ValidationException()
        {
        }

        public ValidationException(string message)
            : this(message, Enumerable.Empty<ValidationError>())
        {
        }

        public ValidationException(IEnumerable<ValidationError> errors)
            : this("One or more validation errors has occurred", errors)
        {
        }

        public ValidationException(string message, IEnumerable<ValidationError> errors)
            : base(message)
        {
            Errors = errors;
        }
    }
}
