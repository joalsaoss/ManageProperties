using ManagerProperties.Application.Utilities.Mediator;

namespace ManagerProperties.Application.UseCases.Properties.Commands.CreateProperty
{
    public class CommandCreateProperty:IRequest<Guid>
    {
        public required Guid OwnerId { get; set; }
        public required string CodeInternal { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required decimal Price { get; set; }
        public required int Year { get; set; }
    }
}
