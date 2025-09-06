using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerProperties.Application.UseCases.PropertyTraces.Queries.GetAllPropertyTraces
{
    public class GetAllPropertyTracesDTO
    {
        public Guid Id { get; set; }
        public required Guid PropertyId { get; set; }
        public required string DateSale { get; set; }
        public required string Name { get; set; } = null!;
        public required decimal Value { get; set; } = 0;
        public required decimal Tax { get; set; } = 0;
    }
}
