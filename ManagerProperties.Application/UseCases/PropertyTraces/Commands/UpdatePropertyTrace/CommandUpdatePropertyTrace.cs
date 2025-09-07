using ManagerProperties.Application.Utilities.Mediator;

namespace ManagerProperties.Application.UseCases.PropertyTraces.Commands.UpdatePropertyTrace
{
    public class CommandUpdatePropertyTrace : IRequest
    {
        public Guid Id { get; set; }
        public required Guid PropertyId { get; set; }
        public required DateTime DateSale { get; set; }
        public string Name { get; set; } = null!;
        public decimal Value { get; set; } = 0;
        public decimal Tax { get; set; } = 0;
    }
}
