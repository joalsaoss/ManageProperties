using ManageProperties.Domain.Exceptions;

namespace ManageProperties.Domain.Common.Guards
{
    public static class DateOfBirthGuard
    {
        /// <summary>
        /// Valida que la fecha de nacimiento no sea futura, edad >= 18 y <= 120.
        /// </summary>
        public static void ValidateOrThrow(DateOnly dob, DateOnly? todayOverride = null)
        {
            var today = todayOverride ?? DateOnly.FromDateTime(DateTime.UtcNow.Date);

            if (dob > today)
                throw new BusinessRulesExceptions("La birthday no puede ser una fecha futura");

            var age = CalculateAge(dob, today);

            if (age < 18)
                throw new BusinessRulesExceptions("El propietario debe ser mayor de edad (>= 18)");

            if (age > 120)
                throw new BusinessRulesExceptions("La birthday no es válida (edad > 120)");
        }

        private static int CalculateAge(DateOnly dob, DateOnly today)
        {
            var age = today.Year - dob.Year;
            if (today < dob.AddYears(age)) age--;
            return age;
        }
    }
}
