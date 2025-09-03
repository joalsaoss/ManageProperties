using ManageProperties.Domain.Exceptions;
using System.Globalization;

namespace ManageProperties.Domain.Common.Parser
{
    public static class DateParser
    {
        /// <summary>
        /// Parsea una fecha en formato exacto yyyy-MM-dd (invariante). Lanza BusinessRulesExceptions si falla.
        /// </summary>
        public static DateOnly ParseOrThrow(string date)
        {
            if (string.IsNullOrWhiteSpace(date))
                throw new BusinessRulesExceptions($"La fecha es obligatoria");

            if (!DateOnly.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                                        DateTimeStyles.None, out var parsed))
            {
                throw new BusinessRulesExceptions($"La fecha debe tener formato yyyy-MM-dd");
            }

            return parsed;
        }
    }
}
