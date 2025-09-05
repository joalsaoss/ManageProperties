using ManagerProperties.Application.Utilities.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ManagerProperties.Application.UseCases.Owners.Commands.UpdateOwner
{
    public class CommandUpdateOwner:IRequest
    {
        public Guid id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string Photo { get; set; }
        public required string Birthday { get; set; }

    }
}
