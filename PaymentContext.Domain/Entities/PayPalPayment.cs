using System;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{
    public class PayPalPayment : Payment
    {
        public PayPalPayment(
            string transactionNumber, 
            DateTime paidDate, 
            DateTime expireDate, 
            decimal total, 
            decimal totalPaid, 
            string payer, 
            Document document, 
            Address address, 
            Email email) : base (paidDate, expireDate, total, totalPaid, payer, document, address, email)
        {
            TransactionNumber = transactionNumber;
        }

        public string TransactionNumber { get; private set; }
    }
}