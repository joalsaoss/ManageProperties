using System.ComponentModel.DataAnnotations;

namespace ManageProperties.API.DTOs.PropertyImages
{
    public class UpdatePropertyImageDTO
    {
        public Guid PropertyId { get; set; }

        [Required]
        [StringLength(50)]
        public required string Image { get; set; }

        [Required]
        [StringLength(1)]
        public required string Enable { get; set; }
    }
}
