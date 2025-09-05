using ManageProperties.Domain.Entities;

namespace ManagerProperties.Application.UseCases.Owners.Queries.GetOwnerDetails
{
    public static class ExtensionMapping
    {
        public static OwnerDetailDTO ADto(this Owner owner)
        {
            var dto = new OwnerDetailDTO()
            {
                Id = owner.Id,
                Name = owner.Name,
                Address = owner.Address,
                Photo = owner.Photo,
                Birthday = owner.Birthday.ToString()
            };
            return dto;
        }
    }
}
