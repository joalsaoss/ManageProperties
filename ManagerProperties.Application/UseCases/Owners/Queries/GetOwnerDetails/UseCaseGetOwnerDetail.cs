using ManagerProperties.Application.Contracts.Repositories;
using ManagerProperties.Application.Exceptions;
using ManagerProperties.Application.Utilities.Mediator;

namespace ManagerProperties.Application.UseCases.Owners.Queries.GetOwnerDetails
{
    public class UseCaseGetOwnerDetail : IRequestHandler<GetOwnerDetail, OwnerDetailDTO>
    {
        private readonly IRepositoryOwners repositoryOwners;
        public UseCaseGetOwnerDetail(IRepositoryOwners repositoryOwners)
        {
            this.repositoryOwners = repositoryOwners;
        }

        public async Task<OwnerDetailDTO> Handle(GetOwnerDetail request)
        {
            var owner = await repositoryOwners.GetById(request.Id);

            if (owner is null)
            {
                throw new NFoundException();
            }

            return owner.ADto();
        }
    }
}
