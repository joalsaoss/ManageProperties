using ManagerProperties.Application.Contracts.Persists;
using ManagerProperties.Application.Contracts.Repositories;
using ManagerProperties.Application.Exceptions;
using ManagerProperties.Application.Utilities.Mediator;

namespace ManagerProperties.Application.UseCases.PropertyTraces.Commands.UpdatePropertyTrace
{
    public class UseCaseUpdatePropertyTrace : IRequestHandler<CommandUpdatePropertyTrace>
    {
        private readonly IRepositoryPropertyTraces repository;
        private readonly IUnitOfWork unitOfWork;

        public UseCaseUpdatePropertyTrace(IRepositoryPropertyTraces repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }
        public async Task Handle(CommandUpdatePropertyTrace request)
        {
            var propertyTrace = await repository.GetById(request.Id);

            if (propertyTrace is null)
                throw new NFoundException();

            propertyTrace.Update(request.PropertyId, 
                request.DateSale, request.Name, request.Value, request.Tax);

            try
            {
                await repository.Update(propertyTrace);
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
