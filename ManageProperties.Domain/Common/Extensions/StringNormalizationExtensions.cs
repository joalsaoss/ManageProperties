using System.Text.RegularExpressions;

namespace ManageProperties.Domain.Common.Extensions
{
    public static class StringNormalizationExtensions
    {
        // Compilados para rendimiento; \s incluye espacios, tabs y saltos de línea.
        private static readonly Regex MultiWhitespace = new(@"\s+", RegexOptions.Compiled);
        private static readonly Regex SpaceBeforePunct = new(@"\s+([,\.])", RegexOptions.Compiled); // "Hola , mundo" -> "Hola, mundo"
        private static readonly Regex PunctSpacing = new(@"([,\.])\s*", RegexOptions.Compiled);      // "Hola,   mundo" -> "Hola, mundo"
        private static readonly Regex DoubleCommas = new(@",\s*,+", RegexOptions.Compiled);          // ",," o ",  ," -> ","

        /// <summary>
        /// Colapsa cualquier secuencia de espacios en uno solo y aplica Trim.
        /// Además: quita espacios antes de comas/puntos, asegura un espacio después, y elimina comas duplicadas.
        /// </summary>
        public static string NormalizeWhitespaceAndPunctuation(this string? s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return string.Empty;

            var result = MultiWhitespace.Replace(s, " ").Trim();
            result = SpaceBeforePunct.Replace(result, "$1");
            result = PunctSpacing.Replace(result, "$1 ");    // deja 1 espacio después
            result = DoubleCommas.Replace(result, ",");

            // Limpieza final por si quedó doble espacio tras ajustes
            result = MultiWhitespace.Replace(result, " ").Trim();
            return result;
        }

        /// <summary>
        /// Solo espacios: colapsa whitespace y Trim (sin tocar puntuación).
        /// </summary>
        public static string NormalizeWhitespace(this string? s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return string.Empty;

            return MultiWhitespace.Replace(s, " ").Trim();
        }
    }
}