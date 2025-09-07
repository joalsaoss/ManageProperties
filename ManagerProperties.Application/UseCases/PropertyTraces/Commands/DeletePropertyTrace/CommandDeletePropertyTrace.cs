using ManagerProperties.Application.Utilities.Mediator;

namespace ManagerProperties.Application.UseCases.PropertyTraces.Commands.DeletePropertyTrace
{
    public class CommandDeletePropertyTrace: IRequest
    {
        public Guid Id { get; set; }
    }
}
