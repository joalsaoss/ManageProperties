namespace ManagerProperties.Application.UseCases.PropertyImages.Queries.GetAllPropertyImage
{
    public class GetAllPropertyImageDTO
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public required string Image { get; set; } = string.Empty;
        public required string Enable { get; set; } = string.Empty;
    }
}
