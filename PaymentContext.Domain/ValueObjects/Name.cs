using Flunt.Validations;
using PaymentContext.Shared.ValueObject;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            
            AddNotifications(new Contract<Name>()
                .Requires()
                .IsLowerThan(FirstName, 3, "Name.FirstName", "Nome deve conter no minimo 3 caracteres")
                .IsLowerThan(LastName, 3, "Name.LastName", "Sobrenome deve conter no minimo 3 caracteres")
                .IsGreaterThan(FirstName, 40, "Name.FirstName", "Nome deve conter até 40 caracteres")
                .IsGreaterThan(LastName, 40, "Name.LastName", "Sobrenome deve conter no minimo 40 caracteres")
            );
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}