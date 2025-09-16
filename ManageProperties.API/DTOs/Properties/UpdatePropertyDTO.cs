using System.ComponentModel.DataAnnotations;

namespace ManageProperties.API.DTOs.Properties
{
    /// <summary>
    /// Objeto de transferencia de datos (DTO) para modificar una propiedad.
    /// </summary>
    public class UpdatePropertyDTO
    {
        /// <summary>
        /// Identificador único de la propiedad.
        /// </summary>
        public Guid id { get; set; }

        /// <summary>
        /// Identificador único del propietario al que pertenece la propiedad.
        /// </summary>
        /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
        public Guid OwnerId { get; set; }

        /// <summary>
        /// Código interno de la propiedad.
        /// </summary>
        /// <remarks>Debe ser único y no superar los 20 caracteres.</remarks>
        /// <example>PROP-001</example>
        [Required]
        [StringLength(20)]
        public required string CodeInternal { get; set; }

        /// <summary>
        /// Nombre de la propiedad.
        /// </summary>
        /// <example>Apartamento en el Poblado</example>
        [Required]
        [StringLength(60)]
        public required string Name { get; set; }

        /// <summary>
        /// Dirección de la propiedad.
        /// </summary>
        /// <example>Carrera 45 #12-34, Medellín</example>
        [Required]
        [StringLength(100)]
        public required string Address { get; set; }

        /// <summary>
        /// Precio de la propiedad en la moneda local.
        /// </summary>
        /// <remarks>Se recomienda enviar el valor con dos decimales.</remarks>
        /// <example>350000000.00</example>
        [Required]
        public required decimal Price { get; set; }

        /// <summary>
        /// Año de construcción de la propiedad.
        /// </summary>
        /// <remarks>Debe ser un valor numérico de cuatro dígitos.</remarks>
        /// <example>2015</example>
        [Required]
        public required int Year { get; set; }
    }
}
