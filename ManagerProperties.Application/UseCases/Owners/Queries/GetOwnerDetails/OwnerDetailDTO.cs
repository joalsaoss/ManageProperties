namespace ManagerProperties.Application.UseCases.Owners.Queries.GetOwnerDetails
{
    public class OwnerDetailDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public byte[] Photo { get; set; } = null!;
        public string Birthday { get; set; } = null!;
    }
}
