using System.ComponentModel.DataAnnotations;

namespace ManageProperties.API.DTOs.Owners
{
    public class UpdateOwnerDTO
    {
        [Required]
        [StringLength(60)]
        public required string Name { get; set; }

        [Required]
        [StringLength(100)]
        public required string Address { get; set; }

        [Required]
        [StringLength(60)]
        public required string Photo { get; set; }

        [Required]
        [StringLength(10)]
        public required string Birthday { get; set; }
    }
}
