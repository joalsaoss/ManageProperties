using ManageProperties.Domain.Exceptions;

namespace ManageProperties.Domain.Entities
{
    public class PropertyImage
    {
        public Guid Id { get; private set; }
        public Guid IdProperty { get; private set; }
        public string File { get; private set; } = null!;
        public string Enable { get; private set; } = null!;
        public Property? Property { get; private set; }

        public PropertyImage(Guid idProperty, string file, string enable)
        {
            if (string.IsNullOrWhiteSpace(file))
            {
                throw new BusinessRulesExceptions($"El archivo es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(enable))
            {
                throw new BusinessRulesExceptions($"El estado es obligatorio");
            }

            Id = Guid.CreateVersion7();
            IdProperty = idProperty;
            File = file;
            Enable = enable;
        }
    }
}
