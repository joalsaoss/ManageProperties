using ManagerProperties.Application.Utilities.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerProperties.Application.UseCases.Properties.Commands.DeleteProperty
{
    public class CommandDeleteProperty: IRequest
    {
        public required Guid Id { get; set; }
    }
}
