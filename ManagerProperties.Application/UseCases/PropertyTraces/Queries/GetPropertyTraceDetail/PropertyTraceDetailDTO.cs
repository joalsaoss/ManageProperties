namespace ManagerProperties.Application.UseCases.PropertyTraces.Queries.GetPropertyTraceDetail
{
    public class PropertyTraceDetailDTO
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public string DateSale { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Value { get; set; } = 0;
        public decimal Tax { get; set; } = 0;
    }
}
