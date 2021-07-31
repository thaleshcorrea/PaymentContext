using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentContext.Tests.ValueObjets
{
    [TestClass]
    public class DocumentTests
    {
        // Red, Green, Refactor
        // 

        [TestMethod]
        public void ShouldReturnErrorWhenCnpjIsInvalid()
        {
            Document document = new("1234", EDocumentType.CNPJ);
            Assert.IsFalse(document.IsValid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenCnpjIsValid()
        {
            Document document = new("76378455000107", EDocumentType.CNPJ);
            Assert.IsTrue(document.IsValid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenCpfIsInvalid()
        {
            Document document = new("1234", EDocumentType.CNPJ);
            Assert.IsFalse(document.IsValid);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("32080718096")]
        [DataRow("78308005020")]
        [DataRow("16652340910")]
        public void ShouldReturnSuccessWhenCpfIsValid(string cpf)
        {
            Document document = new(cpf, EDocumentType.CPF);
            Assert.IsTrue(document.IsValid);
        }
    }
}
