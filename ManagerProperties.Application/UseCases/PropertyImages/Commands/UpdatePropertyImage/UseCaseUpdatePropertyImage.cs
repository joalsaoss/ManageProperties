using ManagerProperties.Application.Contracts.Persists;
using ManagerProperties.Application.Contracts.Repositories;
using ManagerProperties.Application.Exceptions;
using ManagerProperties.Application.Utilities.Mediator;

namespace ManagerProperties.Application.UseCases.PropertyImages.Commands.UpdatePropertyImage
{
    public class UseCaseUpdatePropertyImage : IRequestHandler<CommandUpdatePropertyImage>
    {
        private readonly IRepositoryPropertyImages repository;
        private readonly IUnitOfWork unitOfWork;

        public UseCaseUpdatePropertyImage(IRepositoryPropertyImages repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }
        public async Task Handle(CommandUpdatePropertyImage request)
        {
            var propertyImage = await repository.GetById(request.Id);
            
            if (propertyImage is null)
            {
                throw new NFoundException();
            }

            CancellationToken ct = default;
            var ms = new MemoryStream();
            await request.Bytes.CopyToAsync(ms, ct);

            var ext = Path.GetExtension(request.PhotoFileName ?? "upload");
            var key = $"owners/{request.PropertyId:D}/{DateTime.UtcNow:yyyyMMddHHmmss}_{Guid.NewGuid():N}{ext}";

            propertyImage.Update(request.PropertyId, key, ms.ToArray(), request.ContentType, request.Enable);
            
            try
            {
                await repository.Update(propertyImage);
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
