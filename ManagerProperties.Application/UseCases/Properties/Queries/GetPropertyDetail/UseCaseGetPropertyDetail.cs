using ManagerProperties.Application.Contracts.Repositories;
using ManagerProperties.Application.Exceptions;
using ManagerProperties.Application.Utilities.Mediator;

namespace ManagerProperties.Application.UseCases.Properties.Queries.GetPropertyDetail
{
    public class UseCaseGetPropertyDetail : IRequestHandler<GetPropertyDetail, PropertyDetailDTO>
    {
        private readonly IRepositoryProperties repository;

        public UseCaseGetPropertyDetail(IRepositoryProperties repository)
        {
            this.repository = repository;
        }
        public async Task<PropertyDetailDTO> Handle(GetPropertyDetail request)
        {
            var property = await repository.GetById(request.Id);

            if (property is null)
            {
                throw new NFoundException();
            }

            return property.ToDTO();
        }
    }
}
