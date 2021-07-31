using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        private IList<Subscription> _subscriptions;

        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
            _subscriptions = new List<Subscription>();
            
            AddNotifications(name, document, email);
        }

        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address? Address { get; private set; }
        public IReadOnlyCollection<Subscription> Subscriptions { get => _subscriptions.ToArray(); }

        public void AddSubscription(Subscription subscription)
        {
            bool hasSubscriptionActive = false;
            foreach(var sub in _subscriptions)
            {
                if(sub.Active)
                {
                    hasSubscriptionActive = true;
                }
            }

            AddNotifications(new Contract<Student>()
                .Requires()
                .IsFalse(hasSubscriptionActive, "Student.Subscriptions", "Voc� j� tem uma assinatura ativa");
        }
    }
}