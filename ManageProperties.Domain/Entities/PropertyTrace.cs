using ManageProperties.Domain.Common.Extensions;
using ManageProperties.Domain.Exceptions;

namespace ManageProperties.Domain.Entities
{
    public class PropertyTrace
    {
        public Guid Id { get; private set; }
        public Guid PropertyId { get; private set; }
        public DateTime DateSale { get; private set; }
        public string Name { get; private set; } = null!;
        public decimal Value { get; private set; } = 0;
        public decimal Tax { get; private set; } = 0;
        public Property? Property { get; private set; }

        public PropertyTrace(Guid propertyId, DateTime dateSale, string name, decimal value, decimal tax)
        {
            ApplyBusinessRulesDateSale(dateSale);
            ApplyBusinessRulesName(name);
            ApplyBusinessRulesPrice(value);
            ApplyBusinessRulesTax(tax);

            Id = Guid.CreateVersion7();
            PropertyId = propertyId;
            DateSale = dateSale;
            Name = name.NormalizePersonName();
            Value = value;
            Tax = tax;
        }

        public void Update(Guid propertyId, DateTime dateSale, string name, decimal value, decimal tax)
        {
            ApplyBusinessRulesDateSale(dateSale);
            ApplyBusinessRulesName(name);
            ApplyBusinessRulesPrice(value);
            ApplyBusinessRulesTax(tax);

            PropertyId = propertyId;
            DateSale = dateSale;
            Name = name.NormalizePersonName();
            Value = value;
            Tax = tax;
        }

        private void ApplyBusinessRulesDateSale(DateTime dateSale)
        {
            var today = DateTime.UtcNow.Date;
            if (dateSale > today)
            {
                throw new BusinessRulesExceptions("La fecha de venta no puede ser una fecha futura");
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

        private void ApplyBusinessRulesTax(decimal tax)
        {
            if (decimal.IsNegative(tax))
            {
                throw new BusinessRulesExceptions($"El impuesto debe ser mayor o igual que cero");
            }
        }
    }
}
