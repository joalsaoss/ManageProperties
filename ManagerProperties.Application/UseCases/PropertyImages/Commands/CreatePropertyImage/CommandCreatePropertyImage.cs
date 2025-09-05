using ManagerProperties.Application.Utilities.Mediator;

namespace ManagerProperties.Application.UseCases.PropertyImages.Commands.CreatePropertyImage
{
    public class CommandCreatePropertyImage : IRequest<Guid>
    {
        public required Guid PropertyId { get; set; }
        public required string Image { get; set; } = null!;
        public required string Enable { get; set; } = null!;
    }
}
