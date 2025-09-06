using ManageProperties.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerProperties.Application.UseCases.PropertyTraces.Queries.GetAllPropertyTraces
{
    public static class ExtensionMapping
    {
        public static GetAllPropertyTracesDTO AsGetAllPropertyTracesDTO(this PropertyTrace propertyTrace)
        {
            return new GetAllPropertyTracesDTO
            {
                Id = propertyTrace.Id,
                PropertyId = propertyTrace.PropertyId,
                DateSale = propertyTrace.DateSale.ToString("yyyy-MM-dd"),
                Name = propertyTrace.Name,
                Value = propertyTrace.Value,
                Tax = propertyTrace.Tax
            };
        }
    }
}
