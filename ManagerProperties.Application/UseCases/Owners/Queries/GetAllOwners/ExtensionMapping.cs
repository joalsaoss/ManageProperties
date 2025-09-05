using ManageProperties.Domain.Entities;

namespace ManagerProperties.Application.UseCases.Owners.Queries.GetAllOwners
{
    public static class ExtensionMapping
    {
        public static GetAllOwnersDTO AsGetAllOwnersDTO(this Owner owner)
        {
            return new GetAllOwnersDTO
            {
                id = owner.Id,
                name = owner.Name,
                address = owner.Address,
                photo = owner.Photo,
                birthday = owner.Birthday.ToString("yyyy-mm-dd")
            };
        }
    }
}
