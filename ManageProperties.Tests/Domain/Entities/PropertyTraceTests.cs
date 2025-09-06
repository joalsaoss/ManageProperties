using ManageProperties.Domain.Entities;
using ManageProperties.Domain.Exceptions;

namespace ManageProperties.Tests.Domain.Entities
{
    [TestFixture]
    public class PropertyTraceTests
    {
        private static Guid SomePropertyId() => Guid.NewGuid();

        [Test]
        public void Ctor_DateOnly_lanza_si_dateSale_es_futuro()
        {
            var future = DateTime.UtcNow.Date;

            var ex = Assert.Throws<BusinessRulesExceptions>(
                () => new PropertyTrace(SomePropertyId(), future, "Juan Perez", 100000, 100));

            Assert.That(ex!.Message, Is.EqualTo("La fecha de venta no puede ser una fecha futura"));
        }

        [TestCase("")]
        [TestCase("   ")]
        public void Ctor_DateOnly_lanza_si_name_es_nulo_o_blanco(string name)
        {
            var today = DateTime.UtcNow.Date;

            var ex = Assert.Throws<BusinessRulesExceptions>(
                () => new PropertyTrace(SomePropertyId(), today, name, 100000, 10));

            Assert.That(ex!.Message, Is.EqualTo("El nombre es obligatorio"));
        }

        [TestCase(-0.01)]
        [TestCase(-1)]
        public void Ctor_DateOnly_lanza_si_value_es_negativo(decimal value)
        {
            var today = DateTime.UtcNow.Date;

            var ex = Assert.Throws<BusinessRulesExceptions>(
                () => new PropertyTrace(SomePropertyId(), today, "Juan Perez", value, 10));

            Assert.That(ex!.Message, Is.EqualTo("El valor debe ser mayor o igual que cero"));
        }
        
        [TestCase(-0.01)]
        [TestCase(-1)]
        public void Ctor_DateOnly_lanza_si_tax_es_negativo(decimal tax)
        {
            var today = DateTime.UtcNow.Date;

            var ex = Assert.Throws<BusinessRulesExceptions>(
                () => new PropertyTrace(SomePropertyId(), today, "Juan Perez", 100000, tax));

            Assert.That(ex!.Message, Is.EqualTo("El impuesto debe ser mayor o igual que cero"));
        }

        [Test]
        public void Ctor_DateOnly_permite_value_y_tax_en_cero()
        {
            var today = DateTime.UtcNow.Date;

            var trace = new PropertyTrace(SomePropertyId(), today, "Juan Perez", 0, 0);

            Assert.Multiple(() =>
            {
                Assert.That(trace.Value, Is.EqualTo(0));
                Assert.That(trace.Tax, Is.EqualTo(0));
            });
        }

        // ========== Helper: versión del GUID ==========
        private static int GetGuidVersion(Guid guid)
        {
            var bytes = guid.ToByteArray();
            return (bytes[7] >> 4) & 0x0F;
        }
    }
}