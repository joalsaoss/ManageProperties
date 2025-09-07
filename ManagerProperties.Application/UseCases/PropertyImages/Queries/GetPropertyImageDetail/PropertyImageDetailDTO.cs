namespace ManagerProperties.Application.UseCases.PropertyImages.Queries.GetPropertyImageDetail
{
    public class PropertyImageDetailDTO
    {
        public Guid Id { get; set; }
        public required Guid PropertyId { get; set; }
        public required string PhotoFileName { get; set; }
        public required byte[] Bytes { get; set; }
        public required string ContentType { get; set; }
        public required string Enable { get; set; } = null!;
    }
}
