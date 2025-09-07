using ManageProperties.Domain.Exceptions;

namespace ManageProperties.Tests.Domain.ValueObjects
{
    [TestFixture]
    public class AddressTest
    {
        
        [TestCase("  Calle , 10  ", "Calle, 10")]
        [TestCase("Av.  Las  Palmas 100", "Av. Las Palmas 100")]
        [TestCase("Calle 10,,  A", "Calle 10, A")]
        [TestCase("Calle 10.  Apto 2", "Calle 10. Apto 2")]
        public void Ctor_Normaliza_espacios_y_puntuacion(string input, string expected)
        {
            var address = input;
            Assert.That(address, Is.EqualTo(expected));
        }
    }
}
