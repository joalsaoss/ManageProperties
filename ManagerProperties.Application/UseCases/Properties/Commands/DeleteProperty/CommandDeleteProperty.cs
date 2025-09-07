using ManagerProperties.Application.Utilities.Mediator;

namespace ManagerProperties.Application.UseCases.Properties.Commands.DeleteProperty
{
    public class CommandDeleteProperty: IRequest
    {
        public required Guid Id { get; set; }
    }
}
