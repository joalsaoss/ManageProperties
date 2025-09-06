using ManagerProperties.Application.Utilities.Mediator;

namespace ManagerProperties.Application.UseCases.PropertyImages.Commands.UpdatePropertyImage
{
    public class CommandUpdatePropertyImage : IRequest
    {
        public required Guid Id { get; set; }
        public required Guid PropertyId { get; set; }
        public required string Image { get; set; }
        public required string Enable { get; set; }
    }
}
