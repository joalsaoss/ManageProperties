using ManagerProperties.Application.Contracts.Repositories;
using ManagerProperties.Application.Utilities.Mediator;

namespace ManagerProperties.Application.UseCases.Owners.Queries.GetAllOwners
{
    public class UseCaseGetAllOwner : IRequestHandler<GetAllOwners,
        List<GetAllOwnersDTO>>
    {
        private readonly IRepositoryOwners repository;

        public UseCaseGetAllOwner(IRepositoryOwners repository)
        {
            this.repository = repository;
        }
        public async Task<List<GetAllOwnersDTO>> Handle(GetAllOwners request)
        {
            var owners = await repository.GetAll();
            return owners.Select(o => o.AsGetAllOwnersDTO()).ToList();
        }
    }
}
