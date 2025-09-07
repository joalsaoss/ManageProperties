using System.ComponentModel.DataAnnotations;

namespace ManageProperties.API.DTOs.PropertyImages
{
    public class CreatePropertyImageDTO
    {
        public Guid PropertyId { get; set; }

        //[Required]
        //[StringLength(50)]
        //public required string Image { get; set; }
        
        public required IFormFile? CPhoto { get; set; }

        [Required]
        [StringLength(1)]
        public required string Enable { get; set; }

        
    }
}
