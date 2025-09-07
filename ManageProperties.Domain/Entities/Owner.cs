using ManageProperties.Domain.Common.Extensions;
using ManageProperties.Domain.Exceptions;

namespace ManageProperties.Domain.Entities
{
    public class Owner
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;
        public string Address { get; private set; } = null!;
        public byte[] Photo { get; private set; } = null!;
        public DateTime Birthday { get; private set; }

        public Owner(string name, string address, DateTime birthday)
        {
            ApplyBusinessRuleOwnerString(name);
            ApplyBusinessRuleOwnerString(address);

            Id = Guid.CreateVersion7();
            Name = name.NormalizePersonName();
            Address = address;
            Birthday = birthday;
        }

        public Owner(string name, string address, byte[] photo, DateTime birthday)
        {
            ApplyBusinessRuleOwnerString(name);
            ApplyBusinessRuleOwnerString(address);
            
            Id = Guid.CreateVersion7();
            Name = name.NormalizePersonName();
            Address = address;
            Photo = photo;
            Birthday = birthday;
        }

        public void Update(string name, string address, byte[] photo, DateTime birthday)
        {
            ApplyBusinessRuleOwnerString(name);
            ApplyBusinessRuleOwnerString(address);
            
            Name = name.NormalizePersonName();
            Address = address;
            Photo = photo;
            Birthday = birthday;
        }

        public virtual void UpdateBasic(string name, string address, DateTime birthday)
        {
            Name = name.Trim();
            Address = address.Trim();
            Birthday = birthday;
        }
        
        private void ApplyBusinessRuleOwnerString(string porperty)
        {
            if (string.IsNullOrWhiteSpace(porperty))
                throw new BusinessRulesExceptions($"El {nameof(porperty)} es obligatorio");
        }
    }
}