using ManageProperties.Domain.Entities;
using ManagerProperties.Application.Contracts.Persists;
using ManagerProperties.Application.Contracts.Repositories;
using ManagerProperties.Application.Utilities.Mediator;

namespace ManagerProperties.Application.UseCases.Owners.Commands.CreateOwner
{
    public class UseCaseCreateOwner:IRequestHandler<CommandCreateOwner, Guid>
    {
        private readonly IRepositoryOwners repositoryOwners;
        private readonly IUnitOfWork unitOfWork;
        
        public UseCaseCreateOwner(IRepositoryOwners repositoryOwners, 
            IUnitOfWork unitOfWork)
        {
            this.repositoryOwners = repositoryOwners;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CommandCreateOwner request)
        {
            CancellationToken ct = default;
            var ms = new MemoryStream();
            await request.Bytes.CopyToAsync(ms, ct);

            var owner = new Owner(request.Name, request.Address,
                ms.ToArray(), request.Birthday);
    
            try
            {   
                var response = await repositoryOwners.Create(owner);
                await unitOfWork.Persist();
                return response.Id;
            }
            catch (Exception)
            {
                await unitOfWork.RollBack();
                throw;
            }
            
        }
    }
}
