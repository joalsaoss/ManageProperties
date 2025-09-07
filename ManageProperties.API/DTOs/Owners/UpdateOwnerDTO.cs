using System.ComponentModel.DataAnnotations;

namespace ManageProperties.API.DTOs.Owners
{
    public class UpdateOwnerDTO
    {
        public Guid id { get; set; }

        [Required]
        [StringLength(60)]
        public required string Name { get; set; }

        [Required]
        [StringLength(100)]
        public required string Address { get; set; }

        public required DateTime Birthday { get; set; }

        //Photo content
        public required IFormFile? CPhoto { get; set; }

    }
}
