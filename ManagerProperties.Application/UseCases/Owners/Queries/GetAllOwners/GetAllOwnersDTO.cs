namespace ManagerProperties.Application.UseCases.Owners.Queries.GetAllOwners
{
    public class GetAllOwnersDTO
    {
        public Guid id { get; set; }
        public required string name { get; set; }
        public required string address { get; set; }
        public required string photo { get; set; }
        public required string birthday { get; set; }
    }
}
