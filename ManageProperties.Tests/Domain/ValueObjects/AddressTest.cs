using ManageProperties.Domain.Exceptions;
using ManageProperties.Domain.ValueObjects;

namespace ManageProperties.Tests.Domain.ValueObjects
{
    [TestFixture]
    public class AddressTest
    {
        [TestCase("")]
        [TestCase("   ")]
        public void throwNullOrBlank(string input)
        {
            var ex = Assert.Throws<BusinessRulesExceptions>(() => new Address(input));
            Assert.That(ex!.Message, Is.EqualTo("La dirección es obligatoria"));
        }

        [TestCase("  Calle , 10  ", "Calle, 10")]
        [TestCase("Av.  Las  Palmas 100", "Av. Las Palmas 100")]
        [TestCase("Calle 10,,  A", "Calle 10, A")]
        [TestCase("Calle 10.  Apto 2", "Calle 10. Apto 2")]
        public void Ctor_Normaliza_espacios_y_puntuacion(string input, string expected)
        {
            var address = new Address(input);
            Assert.That(address.value, Is.EqualTo(expected));
        }
    }
}
