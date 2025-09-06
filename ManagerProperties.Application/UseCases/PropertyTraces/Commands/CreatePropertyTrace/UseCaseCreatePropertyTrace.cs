using ManageProperties.Domain.Entities;
using ManagerProperties.Application.Contracts.Persists;
using ManagerProperties.Application.Contracts.Repositories;
using ManagerProperties.Application.Utilities.Mediator;

namespace ManagerProperties.Application.UseCases.PropertyTraces.Commands.CreatePropertyTrace
{
    public class UseCaseCreatePropertyTrace : IRequestHandler<CommandCreatePropertyTrace, Guid>
    {
        private readonly IRepositoryPropertyTraces repository;
        private readonly IUnitOfWork unitOfWork;

        public UseCaseCreatePropertyTrace(IRepositoryPropertyTraces repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(CommandCreatePropertyTrace request)
        {
            var propertyTrace = new PropertyTrace(request.PropertyId, 
                request.DateSale, request.Name, request.Value, request.Tax);

            try
            {
                var response = await repository.Create(propertyTrace);
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
