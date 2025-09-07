using ManageProperties.Domain.Entities;

namespace ManagerProperties.Application.UseCases.PropertyImages.Queries.GetPropertyImageDetail
{
    public static class ExtensionMapping
    {
        public static PropertyImageDetailDTO ToDTO(this PropertyImage propertyImage)
        {
            var dto = new PropertyImageDetailDTO
            {
                Id = propertyImage.Id,
                PropertyId = propertyImage.PropertyId,
                PhotoFileName = propertyImage.PKey,
                Bytes = propertyImage.Bytes,
                ContentType = propertyImage.ContentType,
                Enable = propertyImage.Enable
            };
            return dto;
        }
    }
}
