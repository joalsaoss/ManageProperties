namespace ManagerProperties.Application.UseCases.PropertyImages.Queries.GetAllPropertyImage
{
    public class GetAllPropertyImageDTO
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public required string PhotoFileName { get; set; }
        public required byte[] Bytes { get; set; }
        public required string ContentType { get; set; }
        public required string Enable { get; set; } = string.Empty;
    }
}
