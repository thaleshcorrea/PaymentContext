using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.ValueObjects;
using System;

namespace PaymentContext.Tests.Entities
{
    [TestClass]
    public class StudentTests
    {
        private readonly Name _name;
        private readonly Document _document;
        private readonly Address _address;
        private readonly Email _email;
        private readonly Student _student;
        private readonly Subscription _subscription;

        public StudentTests()
        {
            _name = new("Thales", "Correa");
            _document = new("09435794068", Domain.Enums.EDocumentType.CPF);
            _email = new("thaleshenrique.correa@gmail.com");
            _student = new(_name, _document, _email);
            _address = new("Rua 1", "999", "Top das tops", "Cidade do nada", "Algum lugar", "Lugar nenhum", "0000000");
            _subscription = new(null);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription()
        {
            BoletoPayment payment = new("000000", "0000", DateTime.Now, DateTime.Now.AddDays(30), 100, 100, "Thales Correa", _document, _address, _email);
            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);

            Assert.IsFalse(_student.IsValid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
        {
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);

            Assert.IsFalse(_student.IsValid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenAddSubscription()
        {
            BoletoPayment payment = new("000000", "0000", DateTime.Now, DateTime.Now.AddDays(30), 100, 100, "Thales Correa", _document, _address, _email);
            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.IsValid);
        }
    }
}
