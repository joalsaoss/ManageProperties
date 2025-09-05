using ManageProperties.Domain.Entities;
using ManageProperties.Domain.Exceptions;

namespace ManageProperties.Tests.Domain.Entities
{
    [TestFixture]
    public class PropertyImageTests
    {
        private static Guid SomePropertyId() => Guid.NewGuid();

        // ========= Validaciones =========

        [TestCase("")]
        [TestCase("   ")]
        public void Ctor_debe_lanzar_cuando_file_es_nulo_o_blanco(string file)
        {
            var ex = Assert.Throws<BusinessRulesExceptions>(
                () => new PropertyImage(SomePropertyId(), file, "ENABLED"));

            Assert.That(ex, Is.Not.Null);
            Assert.That(ex!.Message, Is.EqualTo("El archivo es obligatorio"));
        }

        [TestCase("")]
        [TestCase("   ")]
        public void Ctor_debe_lanzar_cuando_enable_es_nulo_o_blanco(string enable)
        {
            var ex = Assert.Throws<BusinessRulesExceptions>(
                () => new PropertyImage(SomePropertyId(), "foto.png", enable));

            Assert.That(ex, Is.Not.Null);
            Assert.That(ex!.Message, Is.EqualTo("El estado es obligatorio"));
        }

        // ========= Asignaciones / Comportamiento =========

        [Test]
        public void Ctor_debe_asignar_propiedades_basicas()
        {
            var idProp = SomePropertyId();
            var file = "foto_frontal.jpg";
            var enable = "ENABLED";

            var img = new PropertyImage(idProp, file, enable);

            Assert.Multiple(() =>
            {
                Assert.That(img.Id, Is.Not.EqualTo(Guid.Empty));
                Assert.That(GetGuidVersion(img.Id), Is.EqualTo(7), "Se esperaba GUID versión 7.");

                Assert.That(img.PropertyId, Is.EqualTo(idProp));
                Assert.That(img.Image, Is.EqualTo(file));
                Assert.That(img.Enable, Is.EqualTo(enable));

                Assert.That(img.Property, Is.Null); // navegación no seteada en ctor
            });
        }

        // ========= Helper: versión del GUID =========
        private static int GetGuidVersion(Guid guid)
        {
            var bytes = guid.ToByteArray();
            return (bytes[7] >> 4) & 0x0F;
        }
    }
}