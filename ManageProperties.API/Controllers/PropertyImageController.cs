using ManageProperties.API.DTOs.PropertyImages;
using ManagerProperties.Application.UseCases.PropertyImages.Commands.CreatePropertyImage;
using ManagerProperties.Application.UseCases.PropertyImages.Commands.UpdatePropertyImage;
using ManagerProperties.Application.UseCases.PropertyImages.Queries.GetAllPropertyImage;
using ManagerProperties.Application.UseCases.PropertyImages.Queries.GetPropertyImageDetail;
using ManagerProperties.Application.UseCases.PropertyTraces.Commands.DeletePropertyTrace;
using ManagerProperties.Application.Utilities.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace ManageProperties.API.Controllers
{
    /// <summary>
    /// Controlador para gestionar las imagenes de las propiedades.
    /// </summary>
    [ApiController]
    [Route("api/propertyimage")]
    public class PropertyImageController:ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Inicializa una nueva instancia del <see cref="PropertyImageController"/>.
        /// </summary>
        /// <param name="mediator">Instancia de <see cref="IMediator"/> para manejar comandos y consultas.</param>
        public PropertyImageController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Obtiene el detalle de una imagen de propiedad específica.
        /// </summary>
        /// <param name="id">Identificador único de la imagen (GUID).</param>
        /// <returns>Un objeto <see cref="PropertyImageDetailDTO"/> con la información detallada de la imagen.</returns>
        /// <response code="200">Devuelve la imagen encontrada.</response>
        /// <response code="404">Si no se encuentra la imagen con el id especificado.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyImageDetailDTO>> Get(Guid id)
        {
            var query = new GetPropertyImageDetail { Id = id};
            var result = await mediator.Send(query);
            return result;
        }

        /// <summary>
        /// Obtiene la lista de todas las imágenes de propiedades registradas.
        /// </summary>
        /// <returns>Una lista de <see cref="GetAllPropertyImageDTO"/>.</returns>
        /// <response code="200">Devuelve la lista de imágenes.</response>
        [HttpGet]
        public async Task<ActionResult<List<GetAllPropertyImageDTO>>> GetAll()
        {
            var query = new GetAllPropertyImage();
            var result = await mediator.Send(query);
            return result;
        }

        /// <summary>
        /// Crea una nueva imagen asociada a una propiedad.
        /// </summary>
        /// <param name="request">Objeto <see cref="CreatePropertyImageDTO"/> con los datos de la imagen, incluyendo el archivo en formato multipart/form-data.</param>
        /// <remarks>
        /// El tamaño máximo permitido para la foto es de 5 MB.
        /// </remarks>
        /// <response code="200">Imagen creada exitosamente.</response>
        /// <response code="400">Si los datos enviados no son válidos.</response>
        [HttpPost]
        [Consumes("multipart/form-data")]
        [RequestSizeLimit(5_000_000)]
        public async Task<IActionResult> Post([FromForm] CreatePropertyImageDTO request)
        {
            Stream? photoStream = null;

            if (request.CPhoto is not null && request.CPhoto.Length > 0)
                photoStream = request.CPhoto.OpenReadStream();

            var command = new CommandCreatePropertyImage
            {
                PropertyId = request.PropertyId,
                PhotoFileName = request.CPhoto.FileName,
                Bytes = photoStream,
                ContentType = request.CPhoto.ContentType,
                Enable = request.Enable
            };

            await mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Actualiza una imagen existente de una propiedad.
        /// </summary>
        /// <param name="request">Objeto <see cref="UpdatePropertyImageDTO"/> con los datos actualizados de la imagen, incluyendo el archivo en formato multipart/form-data.</param>
        /// <remarks>
        /// El tamaño máximo permitido para la foto es de 5 MB.
        /// </remarks>
        /// <response code="200">Imagen actualizada exitosamente.</response>
        /// <response code="400">Si los datos enviados no son válidos.</response>
        /// <response code="404">Si no se encuentra la imagen a actualizar.</response>
        [HttpPut]
        [Consumes("multipart/form-data")]
        [RequestSizeLimit(5_000_000)]
        public async Task<IActionResult> Put([FromForm] UpdatePropertyImageDTO request)
        {
            Stream? photoStream = null;

            if (request.CPhoto is not null && request.CPhoto.Length > 0)
                photoStream = request.CPhoto.OpenReadStream();

            var command = new CommandUpdatePropertyImage
            {
                Id = request.Id,
                PropertyId = request.PropertyId,
                PhotoFileName = request.CPhoto.FileName,
                Bytes = photoStream,
                ContentType = request.CPhoto.ContentType,
                Enable = request.Enable
            };

            await mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Elimina una imagen específica de una propiedad.
        /// </summary>
        /// <param name="id">Identificador único de la imagen (GUID).</param>
        /// <response code="200">Imagen eliminada exitosamente.</response>
        /// <response code="404">Si no se encuentra la imagen con el id especificado.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new CommandDeletePropertyTrace { Id = id };
            await mediator.Send(command);
            return Ok();
        }
    }
}
