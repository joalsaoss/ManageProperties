using ManagerProperties.Application.Utilities.Mediator;

namespace ManagerProperties.Application.UseCases.Owners.Commands.UpdateOwner
{
    public class CommandUpdateOwner:IRequest
    {
        public Guid id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required DateTime Birthday { get; set; }
        public required string PhotoFileName { get; set; }
        public required Stream Bytes { get; set; }
        public required string ContentType { get; set; }

    }
}
