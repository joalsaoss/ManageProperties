using ManageProperties.Domain.Common.Guards;
using ManageProperties.Domain.Exceptions;

namespace ManageProperties.Tests.Common.Guard
{
    [TestFixture]
    public class DateOfBirthGuardTests
    {
        private static readonly DateOnly FixedToday = new DateOnly(2025, 09, 01);

        [Test]
        public void ValidateOrThrow_pasa_con_edad_entre_18_y_120()
        {
            var dob = new DateOnly(1990, 1, 1); // ~35 años
            Assert.DoesNotThrow(() => DateOfBirthGuard.ValidateOrThrow(dob, FixedToday));
        }

        [Test]
        public void ValidateOrThrow_lanza_si_fecha_es_futura()
        {
            var future = FixedToday.AddDays(1);
            var ex = Assert.Throws<BusinessRulesExceptions>(() => DateOfBirthGuard.ValidateOrThrow(future, FixedToday));
            Assert.That(ex!.Message, Is.EqualTo("La birthday no puede ser una fecha futura"));
        }

        [Test]
        public void ValidateOrThrow_lanza_si_es_menor_de_18()
        {
            var under18 = FixedToday.AddYears(-18).AddDays(1); // aún no cumple 18
            var ex = Assert.Throws<BusinessRulesExceptions>(() => DateOfBirthGuard.ValidateOrThrow(under18, FixedToday));
            Assert.That(ex!.Message, Is.EqualTo("El propietario debe ser mayor de edad (>= 18)"));
        }

        [Test]
        public void ValidateOrThrow_permited_exactamente_18()
        {
            var exactly18 = FixedToday.AddYears(-18);
            Assert.DoesNotThrow(() => DateOfBirthGuard.ValidateOrThrow(exactly18, FixedToday));
        }

        [Test]
        public void ValidateOrThrow_lanza_si_edad_mayor_a_120()
        {
            var over120 = FixedToday.AddYears(-121);
            var ex = Assert.Throws<BusinessRulesExceptions>(() => DateOfBirthGuard.ValidateOrThrow(over120, FixedToday));
            Assert.That(ex!.Message, Is.EqualTo("La birthday no es válida (edad > 120)"));
        }
    }
}
