using ManagerProperties.Application.Contracts.Repositories;
using ManagerProperties.Application.Utilities.Mediator;

namespace ManagerProperties.Application.UseCases.PropertyImages.Queries.GetAllPropertyImage
{
    public class UseCaseGetAllProperryImage : IRequestHandler<GetAllPropertyImage, List<GetAllPropertyImageDTO>>
    {
        private readonly IRepositoryPropertyImages repository;

        public UseCaseGetAllProperryImage(IRepositoryPropertyImages repository)
        {
            this.repository = repository;
        }
        public async Task<List<GetAllPropertyImageDTO>> Handle(GetAllPropertyImage request)
        {
            var propertyImage = await repository.GetAll();
            var propertyImageDTO = propertyImage.Select(prop => prop.ToDTO()).ToList();
            return propertyImageDTO;
        }
    }
}
