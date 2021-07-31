using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentContext.Domain.Commands
{
    public class CreateBoletoSubscriptionCommand : Notifiable<Notification>, ICommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }

        public string BarCode { get; set; }
        public string BoletoNumber { get; set; }

        public string PaymentNumber { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPaid { get; set; }
        public string Payer { get; set; }
        public string PayerDocument { get; set; }
        public EDocumentType PayerDocumentType { get; set; }
        public string PayerEmail { get; set; }

        public string Street { get; set; }
        public string Number { get; set; }
        public string Neightborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

        // Fail fast validations
        // Validacoes no que eu conheco como DTOs para validar se os dados da requisicao estao corretos e retorna imediatamente para o cliente
        // antes de chegar no meu dominio, isso evita que algumas requisicoes no banco sejam feitas
        // como uma validacao que possuo na minha regra de negocio, para validar se o cliente ja esta cadastrado pelo cpf
        // sao validacoes simples, somente de dados em branco, valores entre outros, mas dependendo do cenario sao muito boas
        public void Validate()
        {
            AddNotifications(new Contract<CreateBoletoSubscriptionCommand>()
                .Requires()
                .IsGreaterOrEqualsThan(FirstName, 3, "Name.FirstName", "Nome deve conter no minimo 3 caracteres")
                .IsGreaterOrEqualsThan(LastName, 3, "Name.LastName", "Sobrenome deve conter no minimo 3 caracteres")
                .IsLowerOrEqualsThan(FirstName, 40, "Name.FirstName", "Nome deve conter até 40 caracteres")
            );
        }
    }
}
