using ManagerProperties.Application.Contracts.Persists;
using ManagerProperties.Application.Contracts.Repositories;
using ManagerProperties.Application.Utilities.Mediator;

namespace ManagerProperties.Application.UseCases.PropertyTraces.Queries.GetAllPropertyTraces
{
    public class UseCaseGetAllPropertyTrace : IRequestHandler<GetAllPropertyTraces, List<GetAllPropertyTracesDTO>>
    {
        private readonly IRepositoryPropertyTraces repository;
        private readonly IUnitOfWork unitOfWork;

        public UseCaseGetAllPropertyTrace(IRepositoryPropertyTraces repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<GetAllPropertyTracesDTO>> Handle(GetAllPropertyTraces request)
        {
            var propertyTraces = await repository.GetAll();
            return propertyTraces.Select(o => o.AsGetAllPropertyTracesDTO()).ToList();
        }
    }
}
