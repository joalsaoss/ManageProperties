using System.ComponentModel.DataAnnotations;

namespace ManageProperties.API.DTOs.PropertyTraces
{
    /// <summary>
    /// Objeto de transferencia de datos (DTO) para crear un nuevo registro de trazabilidad (historial de venta) de una propiedad.
    /// </summary>
    public class CreatePropertyTraceDTO
    {
        /// <summary>
        /// Identificador único de la propiedad a la que pertenece el registro de venta.
        /// </summary>
        /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
        [Required]
        public required Guid PropertyId { get; set; }

        /// <summary>
        /// Fecha de la venta de la propiedad.
        /// </summary>
        /// <remarks>Debe enviarse en formato ISO 8601 (ejemplo: 2025-09-15).</remarks>
        /// <example>2025-09-15</example>
        [Required]
        public required DateTime DateSale { get; set; }

        /// <summary>
        /// Nombre del comprador o entidad que realiza la adquisición.
        /// </summary>
        /// <remarks>No puede superar los 60 caracteres.</remarks>
        /// <example>Carlos Ramírez</example>
        [Required]
        [StringLength(60)]
        public required string Name { get; set; } = null!;

        /// <summary>
        /// Valor total de la venta de la propiedad.
        /// </summary>
        /// <remarks>Debe incluirse en la moneda local. Se recomienda usar dos decimales.</remarks>
        /// <example>250000000.00</example>
        [Required]
        public required decimal Value { get; set; } = 0;

        /// <summary>
        /// Impuesto aplicado a la transacción.
        /// </summary>
        /// <remarks>Debe expresarse en valor monetario, no en porcentaje.</remarks>
        /// <example>5000000.00</example>
        [Required]
        public required decimal Tax { get; set; } = 0;
    }
}
