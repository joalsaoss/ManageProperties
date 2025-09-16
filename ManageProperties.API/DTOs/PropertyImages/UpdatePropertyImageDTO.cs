using System.ComponentModel.DataAnnotations;

namespace ManageProperties.API.DTOs.PropertyImages
{
    /// <summary>
    /// Objeto de transferencia de datos (DTO) para modificar una imagen de una propiedad.
    /// </summary>
    public class UpdatePropertyImageDTO
    {
        /// <summary>
        /// Identificador único de la imagen de la propiedad.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Identificador único de la propiedad a la que pertenece la imagen.
        /// </summary>
        /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
        public Guid PropertyId { get; set; }

        /// <summary>
        /// Archivo de la fotografía de la propiedad.
        /// </summary>
        /// <remarks>
        /// Debe enviarse como parte de la petición <c>multipart/form-data</c>.  
        /// El tamaño máximo permitido es de 5 MB.  
        /// El campo es obligatorio.
        /// </remarks>
        public required IFormFile? CPhoto { get; set; }

        /// <summary>
        /// Indica si la imagen está habilitada o no.
        /// </summary>
        /// <remarks>
        /// Se espera un valor de un solo carácter (ejemplo: "Y" para habilitada, "N" para deshabilitada).  
        /// Longitud máxima: 1 carácter.
        /// </remarks>
        /// <example>Y</example>
        [Required]
        [StringLength(1)]
        public required string Enable { get; set; }
    }
}