using ManagerProperties.Application.Contracts.Persists;
using ManagerProperties.Application.Contracts.Repositories;
using ManagerProperties.Application.Exceptions;
using ManagerProperties.Application.Utilities.Mediator;

namespace ManagerProperties.Application.UseCases.PropertyTraces.Commands.DeletePropertyTrace
{
    public class UseCaseDeletePropertyTrace : IRequestHandler<CommandDeletePropertyTrace>
    {
        private readonly IRepositoryPropertyTraces repository;
        private readonly IUnitOfWork unitOfWork;

        public UseCaseDeletePropertyTrace(IRepositoryPropertyTraces repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }
        public async Task Handle(CommandDeletePropertyTrace request)
        {
            var propertyTrace = await repository.GetById(request.Id);

            if (propertyTrace is null)
            {
                throw new NFoundException();
            }

            try
            {
                await repository.Delete(propertyTrace);
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
