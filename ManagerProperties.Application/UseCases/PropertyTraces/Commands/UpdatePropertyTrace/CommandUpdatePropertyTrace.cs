using ManagerProperties.Application.Utilities.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
