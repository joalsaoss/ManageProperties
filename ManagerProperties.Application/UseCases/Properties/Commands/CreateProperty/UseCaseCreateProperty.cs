using ManageProperties.Domain.Entities;
using ManagerProperties.Application.Contracts.Persists;
using ManagerProperties.Application.Contracts.Repositories;
using ManagerProperties.Application.Utilities.Mediator;

namespace ManagerProperties.Application.UseCases.Properties.Commands.CreateProperty
{
    public class UseCaseCreateProperty:IRequestHandler<CommandCreateProperty, Guid>
    {
        private readonly IRepositoryProperties repositoryProperties;
        private readonly IUnitOfWork unitOfWork;

        public UseCaseCreateProperty(IRepositoryProperties repositoryProperties, IUnitOfWork unitOfWork)
        {
            this.repositoryProperties = repositoryProperties;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(CommandCreateProperty commandCreate)
        {
            var property = new Property(commandCreate.OwnerId, commandCreate.CodeInternal, 
                commandCreate.Name, commandCreate.Address, commandCreate.Price, commandCreate.Year);

            try
            {
                var response = await repositoryProperties.Create(property);
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
