namespace ManagerProperties.Domain.Commons
{
    public abstract class AuditEntity
    {
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
    }
}
