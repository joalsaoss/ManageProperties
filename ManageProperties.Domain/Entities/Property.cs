using ManageProperties.Domain.Common.Extensions;
using ManageProperties.Domain.Exceptions;

namespace ManageProperties.Domain.Entities
{
    public class Property
    {
        public Guid Id { get; private set; }
        public Guid OwnerId { get; private set; }
        public string CodeInternal { get; private set; } = null!;
        public string Name { get; private set; } = null!;
        public string Address { get; private set; } = null!;
        public decimal Price { get; private set; } = 0;
        public int Year { get; private set; } = 0;
        public Owner? Owner { get; private set; }


        public Property(Guid ownerId, string codeInternal, string name, string address, decimal price, int year)
        {
            ApplyBusinessRulesCodeInternal(codeInternal);
            ApplyBusinessRulesName(name);
            ApplyBusinessRulesName(address);
            ApplyBusinessRulesPrice(price);
            ApplyBusinessRulesYear(year);


            Id = Guid.CreateVersion7();
            OwnerId = ownerId;
            CodeInternal = codeInternal;
            Name = name.NormalizeWhitespaceAndPunctuation();
            Address = address;
            Price = price;
            Year = year;
        }

        public void Update(Guid ownerId, string codeInternal, string name, string address, decimal price, int year)
        {
            ApplyBusinessRulesName(codeInternal);
            ApplyBusinessRulesName(name);
            ApplyBusinessRulesName(address);
            ApplyBusinessRulesPrice(price);
            ApplyBusinessRulesYear(year);

            OwnerId = ownerId;
            CodeInternal = codeInternal;
            Name = name.NormalizeWhitespaceAndPunctuation();
            Address = address;
            Price = price;
            Year = year;
        }

        private void UpdateCodeInternal(string codeInternal)
        {
            ApplyBusinessRulesCodeInternal(codeInternal);
            CodeInternal = codeInternal;
        }

        private void ApplyBusinessRulesCodeInternal(string codeInternal)
        {
            if (string.IsNullOrWhiteSpace(codeInternal))
            {
                throw new BusinessRulesExceptions($"El Código interno es obligatorio");
            }
        }

        private void ApplyBusinessRulesName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new BusinessRulesExceptions($"El nombre es obligatorio");
            }
        }

        private void ApplyBusinessRulesPrice(decimal price)
        {
            if (decimal.IsNegative(price))
            {
                throw new BusinessRulesExceptions($"El precio debe ser mayor o igual que cero");
            }
        }

        private void ApplyBusinessRulesYear(int year)
        {
            if (int.IsNegative(year))
            {
                throw new BusinessRulesExceptions($"El año debe ser mayor o igual que cero");
            }
        }
    }
}
