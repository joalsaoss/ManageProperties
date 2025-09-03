using ManageProperties.Domain.Entities;
using ManageProperties.Domain.Exceptions;
using ManageProperties.Domain.ValueObjects;

namespace ManageProperties.Tests.Domain.Entities
{
    [TestFixture]
    public class OwnerTests
    {
        private static Address ValidAddress()
            => new Address("Cra 123 #45-67, Medellín");

        [TestCase("")]
        [TestCase("   ")]
        public void Ctor_StringBirthday_lanza_cuando_name_es_nulo_o_blanco(string name)
        {
            var ex = Assert.Throws<BusinessRulesExceptions>(
                () => new Owner(name, ValidAddress(), "photo.png", "1990-01-01"));
            Assert.That(ex!.Message, Is.EqualTo("El nombre es obligatorio"));
        }

        [TestCase("")]
        [TestCase("   ")]
        public void Ctor_StringBirthday_lanza_cuando_photo_es_nulo_o_blanco(string photo)
        {
            var ex = Assert.Throws<BusinessRulesExceptions>(
                () => new Owner("Juan", ValidAddress(), photo, "1990-01-01"));
            Assert.That(ex!.Message, Is.EqualTo("La foto es obligatoria"));
        }

        [TestCase("")]
        [TestCase("   ")]
        public void Ctor_StringBirthday_lanza_cuando_birthday_es_nulo_o_blanco(string birthday)
        {
            var ex = Assert.Throws<BusinessRulesExceptions>(
                () => new Owner("Juan", ValidAddress(), "photo.png", birthday));
            Assert.That(ex!.Message, Is.EqualTo("La fecha es obligatoria"));
        }

        [TestCase("01/01/1990")]
        [TestCase("19900101")]
        [TestCase("1990-1-1")]
        public void Ctor_StringBirthday_lanza_cuando_formato_es_invalido(string birthday)
        {
            var ex = Assert.Throws<BusinessRulesExceptions>(
                () => new Owner("Juan", ValidAddress(), "photo.png", birthday));
            Assert.That(ex!.Message, Is.EqualTo("La fecha debe tener formato yyyy-MM-dd"));
        }

        [Test]
        public void Ctor_StringBirthday_convierte_birthday_a_DateOnly()
        {
            var owner = new Owner("Ana", ValidAddress(), "photo.png", "1990-01-01");
            Assert.That(owner.Birthday, Is.EqualTo(new DateOnly(1990, 1, 1)));
        }

        [Test]
        public void Ctor_DateOnly_asigna_directo_y_valida()
        {
            var date = new DateOnly(1990, 1, 1);
            var owner = new Owner("Ana", ValidAddress(), "photo.png", date);
            Assert.That(owner.Birthday, Is.EqualTo(date));
        }

        [Test]
        public void Ctor_Name_se_normaliza()
        {
            var owner = new Owner("  juan   de   la  cruz  ", ValidAddress(), "photo.png", "1990-01-01");
            Assert.That(owner.Name, Is.EqualTo("Juan de la Cruz"));
        }

        [Test]
        public void Ctor_asigna_Address_y_Photo()
        {
            var addr = ValidAddress();
            var owner = new Owner("Ana", addr, "photo.png", "1990-01-01");

            Assert.That(owner.Address, Is.SameAs(addr));
            Assert.That(owner.Photo, Is.EqualTo("photo.png"));
        }

        [Test]
        public void Ctor_genera_Guid_v7_y_no_vacio()
        {
            var owner = new Owner("Ana", ValidAddress(), "photo.png", "1990-01-01");

            Assert.That(owner.Id, Is.Not.EqualTo(Guid.Empty));
            Assert.That(GetGuidVersion(owner.Id), Is.EqualTo(7), "Se esperaba GUID versión 7.");
        }

        [Test]
        public void Ctor_lanza_si_birthday_es_futuro()
        {
            var future = DateOnly.FromDateTime(DateTime.UtcNow.Date).AddDays(1);
            var ex = Assert.Throws<BusinessRulesExceptions>(
                () => new Owner("Ana", ValidAddress(), "photo.png", future));
            Assert.That(ex!.Message, Is.EqualTo("La birthday no puede ser una fecha futura"));
        }

        private static int GetGuidVersion(Guid guid)
        {
            var bytes = guid.ToByteArray();
            return (bytes[7] >> 4) & 0x0F;
        }
    }
}