using FluentValidation.Results;

namespace Base.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public IDictionary<string, string[]> ValidationErrors { get; set; }

        public BadRequestException(string message) : base(message)
        { }

        public BadRequestException(string message, ValidationResult validator) : this(message)
        {
            ValidationErrors = validator.ToDictionary();
        }
    }
}
