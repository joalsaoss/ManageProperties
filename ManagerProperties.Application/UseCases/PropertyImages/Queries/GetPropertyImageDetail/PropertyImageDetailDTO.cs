namespace ManagerProperties.Application.UseCases.PropertyImages.Queries.GetPropertyImageDetail
{
    public class PropertyImageDetailDTO
    {
        public Guid Id { get; set; }
        public required Guid PropertyId { get; set; }
        public required string Image { get; set; } = null!;
        public required string Enable { get; set; } = null!;
    }
}
