using ManageProperties.Domain.Common.Extensions;
using ManageProperties.Domain.Exceptions;
using ManageProperties.Domain.ValueObjects;

namespace ManageProperties.Domain.Entities
{
    public class Property
    {
        public Guid Id { get; private set; }
        public Guid IdOwner { get; private set; }
        public string CodeInternal { get; private set; } = null!;
        public string Name { get; private set; } = null!;
        public Address Address { get; private set; } = null!;
        public double Price { get; private set; } = 0.0;
        public int Year { get; private set; } = 0;
        public Owner? Owner { get; private set; }

        public Property(Guid idOwner, string codeInternal, string name, Address address, double price, int year)
        {
            if (string.IsNullOrWhiteSpace(codeInternal))
            {
                throw new BusinessRulesExceptions($"El Código interno es obligatorio");
            }
            
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new BusinessRulesExceptions($"El nombre es obligatorio");
            }
            
            if (double.IsNaN(price))
            {
                throw new BusinessRulesExceptions($"El precio debe ser un número");
            }

            if (double.IsNegative(price))
            {
                throw new BusinessRulesExceptions($"El precio debe ser mayor o igual que cero");
            }

            if (int.IsNegative(year))
            {
                throw new BusinessRulesExceptions($"El año debe ser mayor o igual que cero");
            }

            Id = Guid.CreateVersion7();
            IdOwner = idOwner;
            CodeInternal = codeInternal;
            Name = name.NormalizeWhitespaceAndPunctuation();
            Address = address;
            Price = price;
            Year = year;

        }

    }
}
