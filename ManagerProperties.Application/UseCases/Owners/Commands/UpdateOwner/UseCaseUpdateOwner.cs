using ManagerProperties.Application.Contracts.Persists;
using ManagerProperties.Application.Contracts.Repositories;
using ManagerProperties.Application.Exceptions;
using ManagerProperties.Application.Utilities.Mediator;

namespace ManagerProperties.Application.UseCases.Owners.Commands.UpdateOwner
{
    public class UseCaseUpdateOwner : IRequestHandler<CommandUpdateOwner>
    {
        private readonly IRepositoryOwners repository;
        private readonly IUnitOfWork unitOfWork;

        public UseCaseUpdateOwner(IRepositoryOwners repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }
        public async Task Handle(CommandUpdateOwner request)
        {
            var owner = await repository.GetById(request.id);
            if (owner is null)
                throw new NFoundException(); 

            owner.Update(request.Name, request.Address, 
                request.Photo, DateTime.Parse(request.Birthday));

            try
            {
                await repository.Update(owner);
                await unitOfWork.Persist();
            }
            catch (Exception)
            {
                await unitOfWork.RollBack();
                throw;
            }
        }
    }
}
