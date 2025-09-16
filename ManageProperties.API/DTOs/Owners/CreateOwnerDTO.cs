using System.ComponentModel.DataAnnotations;

namespace ManageProperties.API.DTOs.Owners
{
    /// <summary>
    /// Objeto de transferencia de datos (DTO) para crear un propietario.
    /// </summary>
    public class CreateOwnerDTO
    {
        /// <summary>
        /// Nombre completo del propietario.
        /// </summary>
        /// <example>Juan Pérez</example>
        [Required]
        [StringLength(60)]
        public required string Name { get; set; }

        /// <summary>
        /// Dirección de residencia del propietario.
        /// </summary>
        /// <example>Calle 123 #45-67, Medellín</example>
        [Required]
        [StringLength(100)]
        public required string Address { get; set; }

        /// <summary>
        /// Fecha de nacimiento del propietario.
        /// </summary>
        /// <remarks>Debe enviarse en formato ISO 8601 (ejemplo: 1990-05-20).</remarks>
        /// <example>1990-05-20</example>
        public required DateTime Birthday { get; set; }

        /// <summary>
        /// Fotografía del propietario en formato archivo.
        /// </summary>
        /// <remarks>
        /// Debe enviarse como parte de la petición <c>multipart/form-data</c>.  
        /// El tamaño máximo permitido es de 5 MB.
        /// </remarks>
        public required IFormFile? CPhoto { get; set; }
    }
}
