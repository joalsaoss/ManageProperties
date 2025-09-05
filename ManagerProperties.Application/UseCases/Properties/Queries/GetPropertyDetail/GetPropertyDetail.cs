using ManagerProperties.Application.Utilities.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerProperties.Application.UseCases.Properties.Queries.GetPropertyDetail
{
    public class GetPropertyDetail:IRequest<PropertyDetailDTO>
    {
        public required Guid Id { get; set; }
    }
}
