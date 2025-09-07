using ManageProperties.Domain.Entities;
using ManageProperties.Domain.Exceptions;

namespace ManageProperties.Tests.Domain.Entities
{
    [TestFixture]
    public class OwnerTests
    {
        private static string address = "Cra 123 #45-67, Medellín";

        [TestCase("")]
        [TestCase("   ")]
        public void Ctor_StringBirthday_lanza_cuando_name_es_nulo_o_blanco(string name)
        {
            var ex = Assert.Throws<BusinessRulesExceptions>(
                () => new Owner(name, address, [], DateTime.Parse("1990-01-01")));
            Assert.That(ex!.Message, Is.EqualTo("El nombre es obligatorio"));
        }

        public void Ctor_StringBirthday_lanza_cuando_photo_es_nulo_o_blanco(byte[] photo)
        {
            var ex = Assert.Throws<BusinessRulesExceptions>(
                () => new Owner("Juan", address, photo, DateTime.Parse("1990-01-01")));
            Assert.That(ex!.Message, Is.EqualTo("La foto es obligatoria"));
        }

        [TestCase("")]
        [TestCase("   ")]
        public void Ctor_StringBirthday_lanza_cuando_birthday_es_nulo_o_blanco(string birthday)
        {
            var ex = Assert.Throws<BusinessRulesExceptions>(
                () => new Owner("Juan", address, [], DateTime.Parse(birthday)));
            Assert.That(ex!.Message, Is.EqualTo("La fecha es obligatoria"));
        }

        [TestCase("01/01/1990")]
        [TestCase("19900101")]
        [TestCase("1990-1-1")]
        public void Ctor_StringBirthday_lanza_cuando_formato_es_invalido(string birthday)
        {
            var ex = Assert.Throws<BusinessRulesExceptions>(
                () => new Owner("Juan", address, [], DateTime.Parse(birthday)));
            Assert.That(ex!.Message, Is.EqualTo("La fecha debe tener formato yyyy-MM-dd"));
        }

        [Test]
        public void Ctor_StringBirthday_convierte_birthday_a_DateOnly()
        {
            var owner = new Owner("Ana", address, [], DateTime.Parse("1990-01-01"));
            Assert.That(owner.Birthday, Is.EqualTo(new DateTime(1990, 1, 1)));
        }

        [Test]
        public void Ctor_DateOnly_asigna_directo_y_valida()
        {
            var date = new DateTime(1990, 1, 1);
            var owner = new Owner("Ana", address, [], date);
            Assert.That(owner.Birthday, Is.EqualTo(date));
        }

        [Test]
        public void Ctor_Name_se_normaliza()
        {
            var owner = new Owner("  juan   de   la  cruz  ", address, [], DateTime.Parse("1990-01-01"));
            Assert.That(owner.Name, Is.EqualTo("Juan de la Cruz"));
        }

        [Test]
        public void Ctor_asigna_Address_y_Photo()
        {
            var addr = address;
            var owner = new Owner("Ana", addr, [], DateTime.Parse("1990-01-01"));

            Assert.That(owner.Address, Is.SameAs(addr));
            Assert.That(owner.Photo, Is.EqualTo("photo.png"));
        }

        [Test]
        public void Ctor_genera_Guid_v7_y_no_vacio()
        {
            var owner = new Owner("Ana", address, [], DateTime.Parse("1990-01-01"));

            Assert.That(owner.Id, Is.Not.EqualTo(Guid.Empty));
            Assert.That(GetGuidVersion(owner.Id), Is.EqualTo(7), "Se esperaba GUID versión 7.");
        }

        private static int GetGuidVersion(Guid guid)
        {
            var bytes = guid.ToByteArray();
            return (bytes[7] >> 4) & 0x0F;
        }
    }
}