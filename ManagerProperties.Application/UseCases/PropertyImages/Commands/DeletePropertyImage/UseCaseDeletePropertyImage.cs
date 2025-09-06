using ManagerProperties.Application.Contracts.Persists;
using ManagerProperties.Application.Contracts.Repositories;
using ManagerProperties.Application.Exceptions;
using ManagerProperties.Application.Utilities.Mediator;

namespace ManagerProperties.Application.UseCases.PropertyImages.Commands.DeletePropertyImage
{
    public class UseCaseDeletePropertyImage : IRequestHandler<CommandDeletePropertyImage>
    {
        private readonly IRepositoryPropertyImages repository;
        private readonly IUnitOfWork unitOfWork;

        public UseCaseDeletePropertyImage(IRepositoryPropertyImages repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }
        public async Task Handle(CommandDeletePropertyImage request)
        {
            var propertyImage = await repository.GetById(request.Id);
            
            if (propertyImage is null)
            {
                throw new NFoundException();
            }

            try
            {
                await repository.Delete(propertyImage);
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
