using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentContext.Tests.Handlers
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            SubscriptionHandler handler = new(new FakeStudentRepository(), new FakeEmailService());
            CreateBoletoSubscriptionCommand command = new CreateBoletoSubscriptionCommand
            {
                FirstName = "Nome",
                LastName = "Sobrenome",
                Document = "99999999999",
                Email = "hello@world.com",
                BarCode = "000000000000",
                BoletoNumber = "000000000000",
                PaymentNumber = "0000000000000",
                PaidDate = DateTime.Now,
                ExpireDate = DateTime.Now.AddMonths(1),
                Total = 100,
                TotalPaid = 100,
                Payer = "Banco",
                PayerDocument = "12345678912",
                PayerDocumentType = Domain.Enums.EDocumentType.CPF,
                PayerEmail = "email@email.com",
                Street = "Rua 1",
                Number = "1",
                Neightborhood = "Bairro 1",
                City = "One City",
                State = "State of Ones",
                Country = "Ones Repulic",
                ZipCode = "00000000"
            };

            handler.Handle(command);
            Assert.AreEqual(false, handler.IsValid);
        }
    }
}
