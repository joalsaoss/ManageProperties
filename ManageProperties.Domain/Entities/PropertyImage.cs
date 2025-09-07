using ManageProperties.Domain.Exceptions;
using ManagerProperties.Domain.Commons;
using System.Net.Mime;

namespace ManageProperties.Domain.Entities
{
    public class PropertyImage: AuditEntity
    {
        public Guid Id { get; private set; }
        public Guid PropertyId { get; private set; }
        public string PKey { get; set; }
        public byte[] Bytes { get; set; }
        public string ContentType { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public string Enable { get; private set; } = null!;
        public Property? Property { get; private set; }

        public PropertyImage(Guid propertyId, string pKey, byte[] bytes, string contentType, string enable)
        {
            ApplyBusinessRuleProperty(pKey);
            ApplyBusinessRuleProperty(contentType);
            ApplyBusinessRuleProperty(enable);

            Id = Guid.CreateVersion7();
            PropertyId = propertyId;
            PKey = pKey;
            Bytes = bytes;
            ContentType = contentType;
            CreatedAtUtc = DateTime.UtcNow;
            Enable = enable;
        }

        public void Update(Guid propertyId, string pKey, byte[] bytes, string contentType, string enable)
        {
            ApplyBusinessRuleProperty(pKey);
            ApplyBusinessRuleProperty(contentType);
            ApplyBusinessRuleProperty(enable);

            PropertyId = propertyId;
            PKey = pKey;
            Bytes = bytes;
            ContentType = contentType;
            CreatedAtUtc = DateTime.UtcNow;
            Enable = enable;
        }

        private void ApplyBusinessRuleProperty(string porperty)
        {
            if (string.IsNullOrWhiteSpace(porperty))
                throw new BusinessRulesExceptions($"El {nameof(porperty)} es obligatorio");
        }
    }
}
