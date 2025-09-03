using ManageProperties.Domain.Common.Extensions;
using System.Globalization;


namespace ManageProperties.Tests.Common.Extensions
{
    [TestFixture]
    public class PersonNameNormalizationExtensionsTests
    {
        [TestCase("", "")]
        [TestCase("   ", "")]
        public void NormalizePersonName_Vacios(string input, string expected)
        => Assert.That(input.NormalizePersonName(), Is.EqualTo(expected));

        [TestCase("  juan   de   la  cruz ", "Juan de la Cruz")]
        [TestCase("maría-josé", "María-José")]
        [TestCase("o'neill", "O'Neill")]
        [TestCase("d'angelo", "D'Angelo")]
        [TestCase("mcgregor", "McGregor")]
        [TestCase("macarthur", "MacArthur")]
        [TestCase("ana   y  carlos", "Ana y Carlos")]
        [TestCase("j. r. r. tolkien", "J. R. R. Tolkien")]
        [TestCase("van   der   waals", "Van der Waals")]
        public void NormalizePersonName_CasosComunes(string input, string expected)
            => Assert.That(input.NormalizePersonName(CultureInfo.GetCultureInfo("es-CO")), Is.EqualTo(expected));

        [Test]
        public void NormalizePersonName_EspaciosGuionesYApostrofes()
            => Assert.That("  Juan -  Pablo  O ' Neill ".NormalizePersonName(),
                           Is.EqualTo("Juan-Pablo O'Neill"));
    }
}

