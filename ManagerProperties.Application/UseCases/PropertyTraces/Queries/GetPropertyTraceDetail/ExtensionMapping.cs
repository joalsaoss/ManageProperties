using ManageProperties.Domain.Entities;

namespace ManagerProperties.Application.UseCases.PropertyTraces.Queries.GetPropertyTraceDetail
{
    public static class ExtensionMapping
    {
        public static PropertyTraceDetailDTO ADto(this PropertyTrace propertyTrace)
        {
            var dto = new PropertyTraceDetailDTO()
            {
                Id = propertyTrace.Id,
                PropertyId = propertyTrace.PropertyId,
                DateSale = propertyTrace.DateSale.ToString(),
                Name = propertyTrace.Name,
                Value = propertyTrace.Value,
                Tax = propertyTrace.Tax
            };
            return dto;
        }
    }
}
