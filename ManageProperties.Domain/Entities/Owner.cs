using ManageProperties.Domain.Common.Extensions;
using ManageProperties.Domain.Exceptions;

namespace ManageProperties.Domain.Entities
{
    public class Owner
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;
        public string Address { get; private set; } = null!;
        public string Photo { get; private set; } = null!;
        public DateTime Birthday { get; private set; }  // <- ahora es DateOnly

        public Owner(string name, string address, string photo, DateTime birthday)
        {
            ApplyBusinessRuleProperty(name);
            ApplyBusinessRuleProperty(address);
            ApplyBusinessRuleProperty(photo);

            //DateOfBirthGuard.ValidateOrThrow(birthday);

            Id = Guid.CreateVersion7();
            Name = name.NormalizePersonName();
            Address = address;
            Photo = photo;
            Birthday = birthday;
        }

        public void Update(string name, string address, string photo, DateTime birthday)
        {
            ApplyBusinessRuleProperty(name);
            ApplyBusinessRuleProperty(address);
            ApplyBusinessRuleProperty(photo);

            //DateOfBirthGuard.ValidateOrThrow(birthday);

            Name = name.NormalizePersonName();
            Address = address;
            Photo = photo;
            Birthday = birthday;
        }

        private void ApplyBusinessRuleProperty(string porperty)
        {
            if (string.IsNullOrWhiteSpace(porperty))
                throw new BusinessRulesExceptions($"El {nameof(porperty)} es obligatorio");
        }
            
    }
}
