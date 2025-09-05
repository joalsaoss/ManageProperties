using ManageProperties.Domain.Entities;

namespace ManagerProperties.Application.UseCases.Properties.Queries.GetPropertyDetail
{
    public static class ExtensionMapping
    {
        public static PropertyDetailDTO ToDTO(this Property property)
        {
            var dto = new PropertyDetailDTO
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
