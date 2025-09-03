using ManageProperties.Domain.Entities;
using ManageProperties.Domain.Exceptions;
using ManageProperties.Domain.ValueObjects;

namespace ManageProperties.Tests.Domain.Entities
{
    [TestFixture]
    public class PropertyTests
    {
        private static Address ValidAddress()
            => new Address("Cra 123 #45-67, Medellín");

        private static Guid SomeOwnerId() => Guid.NewGuid();

        [TestCase("")]
        [TestCase("   ")]
        public void Ctor_debe_lanzar_cuando_codeInternal_es_nulo_o_blanco(string codeInternal)
        {
            var ex = Assert.Throws<BusinessRulesExceptions>(
                () => new Property(SomeOwnerId(), codeInternal, "Apto 101", ValidAddress(), 100_000d, 2020));

            Assert.That(ex!.Message, Is.EqualTo("El Código interno es obligatorio"));
        }

        [TestCase("")]
        [TestCase("   ")]
        public void Ctor_debe_lanzar_cuando_name_es_nulo_o_blanco(string name)
        {
            var ex = Assert.Throws<BusinessRulesExceptions>(
                () => new Property(SomeOwnerId(), "INT-001", name, ValidAddress(), 100_000d, 2020));

            Assert.That(ex!.Message, Is.EqualTo("El nombre es obligatorio"));
        }

        [Test]
        public void Ctor_debe_lanzar_cuando_price_es_NaN()
        {
            var ex = Assert.Throws<BusinessRulesExceptions>(
                () => new Property(SomeOwnerId(), "INT-001", "Apto 101", ValidAddress(), double.NaN, 2020));

            Assert.That(ex!.Message, Is.EqualTo("El precio debe ser un número"));
        }

        [TestCase(-0.01)]
        [TestCase(-1)]
        [TestCase(double.NegativeInfinity)]
        public void Ctor_debe_lanzar_cuando_price_es_negativo(double price)
        {
            var ex = Assert.Throws<BusinessRulesExceptions>(
                () => new Property(SomeOwnerId(), "INT-001", "Apto 101", ValidAddress(), price, 2020));

            Assert.That(ex!.Message, Is.EqualTo("El precio debe ser mayor o igual que cero"));
        }

        [Test]
        public void Ctor_debe_lanzar_cuando_year_es_negativo()
        {
            var ex = Assert.Throws<BusinessRulesExceptions>(
                () => new Property(SomeOwnerId(), "INT-001", "Apto 101", ValidAddress(), 100_000d, -1));

            Assert.That(ex!.Message, Is.EqualTo("El año debe ser mayor o igual que cero"));
        }

        // ===== Asignaciones y normalización =====

        [Test]
        public void Ctor_debe_normalizar_Name_con_NormalizeWhitespaceAndPunctuation()
        {
            var prop = new Property(SomeOwnerId(), "INT-001", "  Apartamento , 101  ", ValidAddress(), 100_000d, 2020);
            Assert.That(prop.Name, Is.EqualTo("Apartamento, 101"));
        }

        [Test]
        public void Ctor_debe_asignar_propiedades_basicas()
        {
            var idOwner = SomeOwnerId();
            var addr = ValidAddress();

            var prop = new Property(idOwner, "INT-001", "Apto 101", addr, 250_000d, 2023);

            Assert.Multiple(() =>
            {
                Assert.That(prop.Id, Is.Not.EqualTo(Guid.Empty));
                Assert.That(GetGuidVersion(prop.Id), Is.EqualTo(7), "Se esperaba GUID versión 7.");
                Assert.That(prop.IdOwner, Is.EqualTo(idOwner));
                Assert.That(prop.CodeInternal, Is.EqualTo("INT-001"));
                Assert.That(prop.Name, Is.EqualTo("Apto 101"));
                Assert.That(prop.Address, Is.SameAs(addr));
                Assert.That(prop.Price, Is.EqualTo(250_000d));
                Assert.That(prop.Year, Is.EqualTo(2023));
                Assert.That(prop.Owner, Is.Null); // se setea posteriormente (agregación)
            });
        }

        // ===== Helpers =====

        private static int GetGuidVersion(Guid guid)
        {
            var bytes = guid.ToByteArray();
            return (bytes[7] >> 4) & 0x0F;
        }
    }
}
