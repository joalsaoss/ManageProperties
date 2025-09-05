using ManageProperties.Domain.Entities;
using ManagerProperties.Application.Contracts.Persists;
using ManagerProperties.Application.Contracts.Repositories;
using ManagerProperties.Application.Utilities.Mediator;

namespace ManagerProperties.Application.UseCases.PropertyImages.Commands.CreatePropertyImage
{
    public class UseCaseCreatePropertyImage : IRequestHandler<CommandCreatePropertyImage, Guid>
    {
        private readonly IRepositoryPropertyImages repository;
        private readonly IUnitOfWork unitOfWork;

        public UseCaseCreatePropertyImage(IRepositoryPropertyImages repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(CommandCreatePropertyImage commandCreatePropertyImage)
        {
            var propertyImage = new PropertyImage(commandCreatePropertyImage.PropertyId, 
                commandCreatePropertyImage.Image, 
                commandCreatePropertyImage.Enable);

            try
            {
                var response = await repository.Create(propertyImage);
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
