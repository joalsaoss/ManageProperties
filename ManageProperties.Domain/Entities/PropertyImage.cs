using ManageProperties.Domain.Exceptions;

namespace ManageProperties.Domain.Entities
{
    public class PropertyImage
    {
        public Guid Id { get; private set; }
        public Guid PropertyId { get; private set; }
        public string Image { get; private set; } = null!;
        public string Enable { get; private set; } = null!;
        public Property? Property { get; private set; }

        public PropertyImage(Guid propertyId, string image, string enable)
        {
            if (string.IsNullOrWhiteSpace(image))
            {
                throw new BusinessRulesExceptions($"El archivo es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(enable))
            {
                throw new BusinessRulesExceptions($"El estado es obligatorio");
            }

            Id = Guid.CreateVersion7();
            PropertyId = propertyId;
            Image = image;
            Enable = enable;
        }
    }
}
