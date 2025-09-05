using ManagerProperties.Application.Contracts.Repositories;
using ManagerProperties.Application.Exceptions;
using ManagerProperties.Application.Utilities.Mediator;

namespace ManagerProperties.Application.UseCases.PropertyImages.Queries.GetPropertyImageDetail
{
    public class UseCaseGetPropertyImageDetail : IRequestHandler<GetPropertyImageDetail, PropertyImageDetailDTO>
    {
        private readonly IRepositoryPropertyImages repository;

        public UseCaseGetPropertyImageDetail(IRepositoryPropertyImages repository)
        {
            this.repository = repository;
        }
        public async Task<PropertyImageDetailDTO> Handle(GetPropertyImageDetail request)
        {
            var propertyImage = await repository.GetById(request.Id);

            if (propertyImage is null)
            {
                throw new NFoundException();
            }
            return propertyImage.ToDTO();
        }
    }
}
