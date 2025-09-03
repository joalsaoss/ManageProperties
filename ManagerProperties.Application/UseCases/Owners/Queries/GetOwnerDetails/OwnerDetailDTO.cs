namespace ManagerProperties.Application.UseCases.Owners.Queries.GetOwnerDetails
{
    public class OwnerDetailDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public string Birthday { get; set; }
    }
}
