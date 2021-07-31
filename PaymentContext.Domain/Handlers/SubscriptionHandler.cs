using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable<Notification>, IHandler<CreateBoletoSubscriptionCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository studentRepository, IEmailService emailService)
        {
            _studentRepository = studentRepository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            // Fail fas validations
            command.Validate();
            if (!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possivel realizar sua assinatura");
            }

            // Verificar se Documento ja esta cadastrado
            // Verificar se email ja esta cadastrado
            AddNotifications(new Contract<SubscriptionHandler>()
                .IsTrue(_studentRepository.DocumentExists(command.Document), "Document", "Documento já cadastrado")
                .IsTrue(_studentRepository.EmailExists(command.Document), "Email", "Email já cadastrado"));

            // Gerar os VOs
            Name name = new(command.FirstName, command.LastName);
            Document document = new(command.PayerDocument, command.PayerDocumentType);
            Email email = new(command.PayerEmail);
            Address address = new(command.Street, command.Number, command.Neightborhood, command.City, command.State, command.Country, command.ZipCode);

            // Gerar as entidades
            Student student = new(name, document, email);
            Subscription subscription = new(DateTime.Now.AddMonths(1));
            BoletoPayment payment = new(
                command.BarCode, 
                command.BoletoNumber, 
                command.PaidDate, 
                command.ExpireDate, 
                command.Total, 
                command.TotalPaid, 
                command.Payer, 
                new Document(command.PayerDocument, command.PayerDocumentType), 
                address, 
                email);

            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar as validacoes
            AddNotifications(name, document, address, student, subscription, payment);

            // Chegar notificaoes
            if(!IsValid)
            {
                return new CommandResult(false, "Nao foi possivel realizar sua assinatura");
            }

            // Salvar as informacoes
            _studentRepository.CreateSubscription(student);

            // Enviar email de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem-vindo", "Assinatura criada com sucesso");

            // retornar informacoes
            return new CommandResult(true, "Assinatura relaizada com sucesso");
        }
    }
}
