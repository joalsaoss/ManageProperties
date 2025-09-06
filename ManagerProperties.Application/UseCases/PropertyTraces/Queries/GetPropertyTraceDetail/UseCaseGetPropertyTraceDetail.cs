using ManagerProperties.Application.Contracts.Repositories;
using ManagerProperties.Application.Exceptions;
using ManagerProperties.Application.Utilities.Mediator;

namespace ManagerProperties.Application.UseCases.PropertyTraces.Queries.GetPropertyTraceDetail
{
    public class UseCaseGetPropertyTraceDetail : IRequestHandler<GetPropertyTraceDetail, PropertyTraceDetailDTO>
    {
        private readonly IRepositoryPropertyTraces repository;

        public UseCaseGetPropertyTraceDetail(IRepositoryPropertyTraces repository)
        {
            this.repository = repository;
        }
        public async Task<PropertyTraceDetailDTO> Handle(GetPropertyTraceDetail request)
        {
            var owner = await repository.GetById(request.Id);

            if (owner is null)
            {
                throw new NFoundException();
            }

            return owner.ADto();
        }
    }
}
