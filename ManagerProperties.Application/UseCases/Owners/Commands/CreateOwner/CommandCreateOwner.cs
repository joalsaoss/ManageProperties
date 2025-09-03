using ManagerProperties.Application.Utilities.Mediator;

namespace ManagerProperties.Application.UseCases.Owners.Commands.CreateOwner
{
    public class CommandCreateOwner:IRequest<Guid>
    {
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string Photo { get; set; }
        public required string Birthday { get; set; }

    }
}
