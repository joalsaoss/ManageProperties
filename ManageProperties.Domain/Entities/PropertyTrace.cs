using ManageProperties.Domain.Common.Extensions;
using ManageProperties.Domain.Common.Parser;
using ManageProperties.Domain.Exceptions;

namespace ManageProperties.Domain.Entities
{
    public class PropertyTrace
    {
        public Guid Id { get; private set; }
        public Guid IdProperty { get; private set; }
        public DateOnly DateSale { get; private set; }
        public string Name { get; private set; } = null!;
        public double Value { get; private set; } = 0.0;
        public double Tax { get; private set; } = 0.0;
        public Property? Property { get; private set; }

        public PropertyTrace(Guid idProperty, string dateSale, string name, double value, double tax)
            : this(idProperty, DateParser.ParseOrThrow(dateSale), name, value, tax)
        {
        }

        public PropertyTrace(Guid idProperty, DateOnly dateSale, string name, double value, double tax)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow.Date);

            if (dateSale > today)
                throw new BusinessRulesExceptions("La fecha de venta no puede ser una fecha futura");

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new BusinessRulesExceptions($"El nombre es obligatorio");
            }

            if (double.IsNaN(value))
            {
                throw new BusinessRulesExceptions($"El valor debe ser un número");
            }

            if (double.IsNegative(value))
            {
                throw new BusinessRulesExceptions($"El valor debe ser mayor o igual que cero");
            }

            if (double.IsNaN(tax))
            {
                throw new BusinessRulesExceptions($"El impuesto debe ser un número");
            }

            if (double.IsNegative(tax))
            {
                throw new BusinessRulesExceptions($"El impuesto debe ser mayor o igual que cero");
            }

            Id = Guid.CreateVersion7();
            IdProperty = idProperty;
            DateSale = dateSale;
            Name = name.NormalizePersonName();
            Value = value;
            Tax = tax;
        }
    }
}
