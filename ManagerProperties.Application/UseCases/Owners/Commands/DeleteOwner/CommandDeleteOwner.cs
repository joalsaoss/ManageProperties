using ManagerProperties.Application.Utilities.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerProperties.Application.UseCases.Owners.Commands.DeleteOwner
{
    public class CommandDeleteOwner: IRequest
    {
        public Guid Id { get; set; }
    }
}
