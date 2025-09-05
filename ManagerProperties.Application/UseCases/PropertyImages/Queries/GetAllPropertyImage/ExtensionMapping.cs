using ManageProperties.Domain.Entities;

namespace ManagerProperties.Application.UseCases.PropertyImages.Queries.GetAllPropertyImage
{
    public static class ExtensionMapping
    {
        public static GetAllPropertyImageDTO ToDTO(this PropertyImage propertyImage)
        {
            var dto = new GetAllPropertyImageDTO
            {
                Id = propertyImage.Id,
                PropertyId = propertyImage.PropertyId,
                Image = propertyImage.Image,
                Enable = propertyImage.Enable
            };
            return dto;
        }
    }
}
