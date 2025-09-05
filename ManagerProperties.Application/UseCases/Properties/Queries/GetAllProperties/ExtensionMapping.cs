using ManageProperties.Domain.Entities;

namespace ManagerProperties.Application.UseCases.Properties.Queries.GetAllProperties
{
    public static class ExtensionMapping
    {
        public static GetAllPropertiesDTO ToDTO(this Property property)
        {
            var dto = new GetAllPropertiesDTO
            {
                Id = property.Id,
                OwnerId = property.OwnerId,
                CodeInternal = property.CodeInternal,
                Name = property.Name,
                Address = property.Address,
                Price = property.Price,
                Year = property.Year
            };
            return dto;
        }
    }
}
