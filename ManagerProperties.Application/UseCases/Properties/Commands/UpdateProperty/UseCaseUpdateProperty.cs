using ManagerProperties.Application.Contracts.Persists;
using ManagerProperties.Application.Contracts.Repositories;
using ManagerProperties.Application.Exceptions;
using ManagerProperties.Application.Utilities.Mediator;

namespace ManagerProperties.Application.UseCases.Properties.Commands.UpdateProperty
{
    public class UseCaseUpdateProperty : IRequestHandler<CommandUpdateProperty>
    {
        private readonly IRepositoryProperties repository;
        private readonly IUnitOfWork unitOfWork;

        public UseCaseUpdateProperty(IRepositoryProperties repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }
        public async Task Handle(CommandUpdateProperty request)
        {
            var property = await repository.GetById(request.Id);

            if (property is null)
            {
                throw new NFoundException();
            }

            property.Update(request.OwnerId, request.CodeInternal,
                request.Name, request.Address, request.Price, request.Year);

            try
            {
                await repository.Update(property);
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
