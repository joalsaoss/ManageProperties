using System.ComponentModel.DataAnnotations;

namespace ManageProperties.API.DTOs.Properties
{
    public class UpdatePropertyDTO
    {
        public Guid OwnerId { get; set; }

        [Required]
        [StringLength(20)]
        public required string CodeInternal { get; set; }

        [Required]
        [StringLength(60)]
        public required string Name { get; set; }

        [Required]
        [StringLength(100)]
        public required string Address { get; set; }

        [Required]
        public required decimal Price { get; set; }

        [Required]
        public required int Year { get; set; }
    }
}
