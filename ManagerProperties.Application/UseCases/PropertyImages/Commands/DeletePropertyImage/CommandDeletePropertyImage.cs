using ManagerProperties.Application.Utilities.Mediator;

namespace ManagerProperties.Application.UseCases.PropertyImages.Commands.DeletePropertyImage
{
    public class CommandDeletePropertyImage : IRequest
    {
        public required Guid Id { get; set; }
    }
}
