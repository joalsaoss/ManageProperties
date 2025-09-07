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
        public async Task<Guid> Handle(CommandCreatePropertyImage request)
        {
            CancellationToken ct = default;
            var ms = new MemoryStream();
            await request.Bytes.CopyToAsync(ms, ct);

            var ext = Path.GetExtension(request.PhotoFileName ?? "upload");
            var key = $"owners/{request.PropertyId:D}/{DateTime.UtcNow:yyyyMMddHHmmss}_{Guid.NewGuid():N}{ext}";

            var propertyImage = new PropertyImage(request.PropertyId, 
                key, ms.ToArray(), request.ContentType, request.Enable);

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
