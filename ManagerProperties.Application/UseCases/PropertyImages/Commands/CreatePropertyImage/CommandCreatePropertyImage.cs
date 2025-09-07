using ManagerProperties.Application.Utilities.Mediator;

namespace ManagerProperties.Application.UseCases.PropertyImages.Commands.CreatePropertyImage
{
    public class CommandCreatePropertyImage : IRequest<Guid>
    {
        public required Guid PropertyId { get; set; }
        public required string PhotoFileName { get; set; }
        public required Stream Bytes { get; set; }
        public required string ContentType { get; set; }
        public required string Enable { get; set; } = null!;
    }
}
