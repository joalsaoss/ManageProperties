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
            var future = DateOnly.FromDateTime(DateTime.UtcNow.Date).AddDays(1);

            var ex = Assert.Throws<BusinessRulesExceptions>(
                () => new PropertyTrace(SomePropertyId(), future, "Juan Perez", 100_000d, 10_000d));

            Assert.That(ex!.Message, Is.EqualTo("La fecha de venta no puede ser una fecha futura"));
        }

        [TestCase("")]
        [TestCase("   ")]
        public void Ctor_DateOnly_lanza_si_name_es_nulo_o_blanco(string name)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow.Date);

            var ex = Assert.Throws<BusinessRulesExceptions>(
                () => new PropertyTrace(SomePropertyId(), today, name, 100_000d, 10_000d));

            Assert.That(ex!.Message, Is.EqualTo("El nombre es obligatorio"));
        }

        [Test]
        public void Ctor_DateOnly_lanza_si_value_es_NaN()
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow.Date);

            var ex = Assert.Throws<BusinessRulesExceptions>(
                () => new PropertyTrace(SomePropertyId(), today, "Juan Perez", double.NaN, 10_000d));

            Assert.That(ex!.Message, Is.EqualTo("El valor debe ser un número"));
        }

        [TestCase(-0.01)]
        [TestCase(-1)]
        [TestCase(double.NegativeInfinity)]
        public void Ctor_DateOnly_lanza_si_value_es_negativo(double value)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow.Date);

            var ex = Assert.Throws<BusinessRulesExceptions>(
                () => new PropertyTrace(SomePropertyId(), today, "Juan Perez", value, 10_000d));

            Assert.That(ex!.Message, Is.EqualTo("El valor debe ser mayor o igual que cero"));
        }

        [Test]
        public void Ctor_DateOnly_lanza_si_tax_es_NaN()
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow.Date);

            var ex = Assert.Throws<BusinessRulesExceptions>(
                () => new PropertyTrace(SomePropertyId(), today, "Juan Perez", 100_000d, double.NaN));

            Assert.That(ex!.Message, Is.EqualTo("El impuesto debe ser un número"));
        }

        [TestCase(-0.01)]
        [TestCase(-1)]
        [TestCase(double.NegativeInfinity)]
        public void Ctor_DateOnly_lanza_si_tax_es_negativo(double tax)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow.Date);

            var ex = Assert.Throws<BusinessRulesExceptions>(
                () => new PropertyTrace(SomePropertyId(), today, "Juan Perez", 100_000d, tax));

            Assert.That(ex!.Message, Is.EqualTo("El impuesto debe ser mayor o igual que cero"));
        }

        [Test]
        public void Ctor_DateOnly_permite_value_y_tax_en_cero()
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow.Date);

            var trace = new PropertyTrace(SomePropertyId(), today, "Juan Perez", 0d, 0d);

            Assert.Multiple(() =>
            {
                Assert.That(trace.Value, Is.EqualTo(0d));
                Assert.That(trace.Tax, Is.EqualTo(0d));
            });
        }

        // ========== Comportamiento / Asignaciones ==========

        [Test]
        public void Ctor_StringDate_parsea_fecha_y_asigna_campos()
        {
            var id = SomePropertyId();
            var trace = new PropertyTrace(id, "2024-08-01", "  juan   perez  ", 123_456.78d, 5_432.1d);

            Assert.Multiple(() =>
            {
                Assert.That(trace.Id, Is.Not.EqualTo(Guid.Empty));
                Assert.That(GetGuidVersion(trace.Id), Is.EqualTo(7), "Se esperaba GUID versión 7.");
                Assert.That(trace.IdProperty, Is.EqualTo(id));
                Assert.That(trace.DateSale, Is.EqualTo(new DateOnly(2024, 8, 1)));
                Assert.That(trace.Name, Is.EqualTo("Juan Perez")); // NormalizePersonName()
                Assert.That(trace.Value, Is.EqualTo(123_456.78d));
                Assert.That(trace.Tax, Is.EqualTo(5_432.1d));
                Assert.That(trace.Property, Is.Null); // navegación no seteada en ctor
            });
        }

        [Test]
        public void Ctor_DateOnly_asigna_campos_correctamente()
        {
            var id = SomePropertyId();
            var date = new DateOnly(2023, 12, 31);
            var trace = new PropertyTrace(id, date, "Ana María", 200_000d, 8_000d);

            Assert.Multiple(() =>
            {
                Assert.That(trace.Id, Is.Not.EqualTo(Guid.Empty));
                Assert.That(GetGuidVersion(trace.Id), Is.EqualTo(7));
                Assert.That(trace.IdProperty, Is.EqualTo(id));
                Assert.That(trace.DateSale, Is.EqualTo(date));
                Assert.That(trace.Name, Is.EqualTo("Ana María"));
                Assert.That(trace.Value, Is.EqualTo(200_000d));
                Assert.That(trace.Tax, Is.EqualTo(8_000d));
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