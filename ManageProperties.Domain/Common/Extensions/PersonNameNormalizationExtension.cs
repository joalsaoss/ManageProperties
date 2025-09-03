using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace ManageProperties.Domain.Common.Extensions
{
    public static class PersonNameNormalizationExtensions
    {
        // \s -> whitespace; compiladas por rendimiento
        private static readonly Regex MultiWhitespace = new(@"\s+", RegexOptions.Compiled);
        private static readonly Regex TightHyphen = new(@"\s*-\s*", RegexOptions.Compiled);      // "Juan -  Pablo" -> "Juan-Pablo"
        private static readonly Regex TightApostrophe = new(@"\s*'\s*", RegexOptions.Compiled);  // "O ' Neill" -> "O'Neill"

        // Palabras "menores" que suelen ir en minúscula cuando no son primera ni última
        private static readonly HashSet<string> MinorWords = new(StringComparer.OrdinalIgnoreCase)
        {
            "de","del","la","las","los","y","e",
            "da","das","do","dos","di","du",
            "van","von","bin","ibn","der","den","ter","ten","zu","zum","zur"
        };

        /// <summary>
        /// Normaliza nombres propios: espacios, guiones/apóstrofes, capitalización por tokens
        /// (p. ej. "  juan   de   la  cruz  " -> "Juan de la Cruz", "maría-josé" -> "María-José", "o'neill" -> "O'Neill").
        /// </summary>
        public static string NormalizePersonName(this string? raw, CultureInfo? culture = null)
        {
            if (string.IsNullOrWhiteSpace(raw))
                return string.Empty;

            culture ??= CultureInfo.GetCultureInfo("es-CO");

            // 1) Unicode NFC + colapso de espacios
            var s = raw.Normalize(NormalizationForm.FormC);
            s = MultiWhitespace.Replace(s, " ").Trim();

            // 2) Espacios alrededor de guiones y apóstrofes
            s = TightHyphen.Replace(s, "-");
            s = TightApostrophe.Replace(s, "'");

            // 3) Tokenización por espacio y capitalización
            var tokens = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tokens.Length; i++)
            {
                var token = tokens[i];

                // Iniciales tipo "j." -> "J."
                if (Regex.IsMatch(token, @"^[A-Za-z]\.$"))
                {
                    tokens[i] = char.ToUpper(token[0], culture) + ".";
                    continue;
                }

                // Palabras con guion: capitaliza cada parte
                if (token.Contains('-'))
                {
                    var parts = token.Split('-', StringSplitOptions.RemoveEmptyEntries);
                    for (int p = 0; p < parts.Length; p++)
                        parts[p] = CapitalizeNamePart(parts[p], culture, forceCapitalize: true);
                    tokens[i] = string.Join('-', parts);
                    continue;
                }

                // Palabras con apóstrofe: capitaliza cada parte
                if (token.Contains('\''))
                {
                    var parts = token.Split('\'', StringSplitOptions.RemoveEmptyEntries);
                    for (int p = 0; p < parts.Length; p++)
                        parts[p] = CapitalizeNamePart(parts[p], culture, forceCapitalize: true);
                    tokens[i] = string.Join('\'', parts);
                    continue;
                }

                // Partículas menores (no primera ni última)
                if (i > 0 && i < tokens.Length - 1 && MinorWords.Contains(token))
                {
                    tokens[i] = token.ToLower(culture);
                    continue;
                }

                // Caso general
                tokens[i] = CapitalizeNamePart(token, culture, forceCapitalize: true);
            }

            return string.Join(' ', tokens);
        }

        private static string CapitalizeNamePart(string part, CultureInfo culture, bool forceCapitalize)
        {
            if (string.IsNullOrEmpty(part)) return part;

            // Normaliza a minúsculas de la cultura
            var lower = part.ToLower(culture);

            // Regla "Mc" (p. ej. McDonald)
            if (lower.StartsWith("mc") && lower.Length > 2 && char.IsLetter(lower[2]))
                return "Mc" + char.ToUpper(lower[2], culture) + lower[3..];

            // Regla "Mac" (opcional; comenta si no la deseas)
            if (lower.StartsWith("mac") && lower.Length > 3 && char.IsLetter(lower[3]))
                return "Mac" + char.ToUpper(lower[3], culture) + lower[4..];

            // Capitaliza primera letra
            return char.ToUpper(lower[0], culture) + (lower.Length > 1 ? lower[1..] : string.Empty);
        }
    }
}