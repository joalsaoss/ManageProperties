using ManagerProperties.Application.Utilities.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerProperties.Application.UseCases.PropertyTraces.Commands.DeletePropertyTrace
{
    public class CommandDeletePropertyTrace: IRequest
    {
        public Guid Id { get; set; }
    }
}
