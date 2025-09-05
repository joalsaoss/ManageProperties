namespace ManagerProperties.Application.UseCases.Properties.Queries.GetAllProperties
{
    public class GetAllPropertiesDTO
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public required string CodeInternal { get; set; } = string.Empty;
        public required string Name { get; set; } = string.Empty;
        public required string Address { get; set; } = string.Empty;
        public required decimal Price { get; set; }
        public required int Year { get; set; }

    }
}
