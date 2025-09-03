using ManageProperties.Domain.Common.Extensions;
using ManageProperties.Domain.Common.Guards;
using ManageProperties.Domain.Common.Parser;
using ManageProperties.Domain.Exceptions;
using ManageProperties.Domain.ValueObjects;

namespace ManageProperties.Domain.Entities
{
    public class Owner
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;
        public Address Address { get; private set; } = null!;
        public string Photo { get; private set; } = null!;
        public DateOnly Birthday { get; private set; }  // <- ahora es DateOnly

        // Conveniencia: recibe string, parsea y delega
        public Owner(string name, Address address, string photo, string birthday)
            : this(name, address, photo, DateParser.ParseOrThrow(birthday))
        { }

        // Canónico: recibe DateOnly y valida reglas de dominio
        public Owner(string name, Address address, string photo, DateOnly birthday)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new BusinessRulesExceptions($"El nombre es obligatorio");

            if (string.IsNullOrWhiteSpace(photo))
                throw new BusinessRulesExceptions($"La foto es obligatoria");

            DateOfBirthGuard.ValidateOrThrow(birthday);

            Id = Guid.CreateVersion7();
            Name = name.NormalizePersonName();
            Address = address;
            Photo = photo;
            Birthday = birthday;
        }
    }
}
