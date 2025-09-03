using ManageProperties.Domain.Common.Extensions;
using ManageProperties.Domain.Exceptions;

namespace ManageProperties.Domain.ValueObjects
{
    public record Address(string value)
    {
        public string value { get; init; } =
            string.IsNullOrWhiteSpace(value)
                ? throw new BusinessRulesExceptions($"La dirección es obligatoria")
                : value.NormalizeWhitespaceAndPunctuation();
    }
}
