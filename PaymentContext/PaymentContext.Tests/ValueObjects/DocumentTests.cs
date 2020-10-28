using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.ValueObjects
{
    [TestClass]
    public class DocumentTests
    {
        //Red, Green, Refectory
        [TestMethod]
        public void DadoCNPJComMaisDe14CaractersDeveRetornarUmErro()
        {
            var doc = new Document("123", EDocumentType.CNPJ);
            Assert.IsTrue(doc.Invalid);

        }

        [TestMethod]
        public void DadoCNPJValidoDeveRetornarSucesso()
        {
            var doc = new Document("12345678901234", EDocumentType.CNPJ);
            Assert.IsTrue(doc.Valid);
        }
        
        [TestMethod]
        public void DadoCpfComMaisDe14CaractersDeveRetornarUmErro()
        {
            var doc = new Document("1231", EDocumentType.CPF);
            Assert.IsTrue(doc.Invalid);
        }

        [TestMethod]
        public void DadoCPFValidoDeveRetornarSucesso()
        {
            var doc = new Document("05615237417", EDocumentType.CPF);
            Assert.IsTrue(doc.Valid);
        }

    }
}