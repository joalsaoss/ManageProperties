using ManagerProperties.Application.Utilities.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerProperties.Application.UseCases.PropertyImages.Queries.GetPropertyImageDetail
{
    public class GetPropertyImageDetail: IRequest<PropertyImageDetailDTO>
    {
        public required Guid Id { get; set; }
    }
}
