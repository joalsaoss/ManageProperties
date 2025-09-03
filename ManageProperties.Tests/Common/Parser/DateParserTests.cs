using ManageProperties.Domain.Exceptions;
using ManageProperties.Domain.Common.Parser;

namespace ManageProperties.Tests.Common.Parser
{
    [TestFixture]
    public class DateParserTests
    {
        [Test]
        public void ParseOrThrow_devuelve_DateOnly_cuando_formato_es_valido()
        {
            var result = DateParser.ParseOrThrow("2000-02-29"); // bisiesto válido
            Assert.That(result, Is.EqualTo(new DateOnly(2000, 2, 29)));
        }

        [TestCase("")]
        [TestCase("   ")]
        public void ParseOrThrow_lanza_cuando_es_nulo_o_blanco(string input)
        {
            var ex = Assert.Throws<BusinessRulesExceptions>(() => DateParser.ParseOrThrow(input));
            Assert.That(ex!.Message, Is.EqualTo("La fecha es obligatoria"));
        }

        [TestCase("01/01/1990")] // dd/MM/yyyy
        [TestCase("1990/01/01")] // yyyy/MM/dd
        [TestCase("19900101")]   // sin separadores
        [TestCase("1990-1-1")]   // sin padding
        [TestCase("1990-02-30")] // fecha inválida
        [TestCase(" 1990-01-01 ")] // espacios alrededor -> falla (formato exacto)
        public void ParseOrThrow_lanza_cuando_formato_es_invalido(string input)
        {
            var ex = Assert.Throws<BusinessRulesExceptions>(() => DateParser.ParseOrThrow(input));
            Assert.That(ex!.Message, Is.EqualTo("La fecha debe tener formato yyyy-MM-dd"));
        }
    }
}
