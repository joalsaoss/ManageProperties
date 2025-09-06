using System.ComponentModel.DataAnnotations;

namespace ManageProperties.API.DTOs.PropertyTraces
{
    public class CreatePropertyTraceDTO
    {
        [Required]
        public required Guid PropertyId { get; set; }

        [Required]
        public required DateTime DateSale { get; set; }

        [Required]
        [StringLength(60)]
        public required string Name { get; set; } = null!;

        [Required]
        public required decimal Value { get; set; } = 0;

        [Required]
        public required decimal Tax { get; set; } = 0;

    }
}
