using ManageProperties.API.DTOs.Owners;
using ManagerProperties.Application.UseCases.Owners.Commands.CreateOwner;
using ManagerProperties.Application.UseCases.Owners.Commands.DeleteOwner;
using ManagerProperties.Application.UseCases.Owners.Commands.UpdateOwner;
using ManagerProperties.Application.UseCases.Owners.Queries.GetAllOwners;
using ManagerProperties.Application.UseCases.Owners.Queries.GetOwnerDetails;
using ManagerProperties.Application.Utilities.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace ManageProperties.API.Controllers
{
    /// <summary>
    /// Controlador para gestionar las propietarios.
    /// </summary>
    [ApiController]
    [Route("api/owners")]
    public class OwnersController:ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Inicializa una nueva instancia del <see cref="OwnersController"/>.
        /// </summary>
        /// <param name="mediator">Instancia de <see cref="IMediator"/> para manejar comandos y consultas.</param>
        public OwnersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Obtiene el detalle de un propietario específico.
        /// </summary>
        /// <param name="id">Identificador único del propietario (GUID).</param>
        /// <returns>Un objeto <see cref="OwnerDetailDTO"/> con la información detallada del propietario.</returns>
        /// <response code="200">Devuelve el propietario encontrado.</response>
        /// <response code="404">Si no se encuentra el propietario con el id especificado.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<OwnerDetailDTO>> Get(Guid id)
        {
            var query = new GetOwnerDetail { Id = id};
            var result = await mediator.Send(query);
            return result;
        }

        /// <summary>
        /// Obtiene la lista de todos los propietarios registrados.
        /// </summary>
        /// <returns>Una lista de <see cref="GetAllOwnersDTO"/>.</returns>
        /// <response code="200">Devuelve la lista de propietarios.</response>
        [HttpGet]
        public async Task<ActionResult<List<GetAllOwnersDTO>>> GetAll()
        {
            var query = new GetAllOwners();
            var result = await mediator.Send(query);
            return result;
        }

        /// <summary>
        /// Crea un nuevo propietario.
        /// </summary>
        /// <param name="request">Objeto <see cref="CreateOwnerDTO"/> con los datos del propietario, incluyendo la foto en formato multipart/form-data.</param>
        /// <remarks>
        /// El tamaño máximo permitido para la foto es de 5 MB.
        /// </remarks>
        /// <response code="200">Propietario creado exitosamente.</response>
        /// <response code="400">Si los datos enviados no son válidos.</response>
        [HttpPost]
        [Consumes("multipart/form-data")]
        [RequestSizeLimit(5_000_000)]
        public async Task<IActionResult> Post([FromForm] CreateOwnerDTO request)
        {
            Stream? photoStream = null;

            if (request.CPhoto is not null && request.CPhoto.Length > 0)
                photoStream = request.CPhoto.OpenReadStream();
            
            var command = new CommandCreateOwner
            {
                Name = request.Name,
                Address = request.Address,
                Birthday = request.Birthday,
                PhotoFileName = request.CPhoto.FileName,
                Bytes = photoStream,
                ContentType = request.CPhoto.ContentType,
            };

            await mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Actualiza la información de un propietario existente.
        /// </summary>
        /// <param name="request">Objeto <see cref="UpdateOwnerDTO"/> con los datos actualizados del propietario, incluyendo la foto en formato multipart/form-data.</param>
        /// <remarks>
        /// El tamaño máximo permitido para la foto es de 5 MB.
        /// </remarks>
        /// <response code="200">Propietario actualizado exitosamente.</response>
        /// <response code="400">Si los datos enviados no son válidos.</response>
        /// <response code="404">Si no se encuentra el propietario a actualizar.</response>
        [HttpPut]
        [Consumes("multipart/form-data")]
        [RequestSizeLimit(5_000_000)]
        public async Task<IActionResult> Put([FromForm] UpdateOwnerDTO request)
        {
            Stream? photoStream = null;

            if (request.CPhoto is not null && request.CPhoto.Length > 0)
                photoStream = request.CPhoto.OpenReadStream();

            var command = new CommandUpdateOwner 
            {
                id = request.id,
                Name = request.Name,
                Address = request.Address,
                Birthday = request.Birthday,
                PhotoFileName = request.CPhoto.FileName,
                Bytes = photoStream,
                ContentType = request.CPhoto.ContentType,
            };

            await mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Elimina un propietario específico.
        /// </summary>
        /// <param name="id">Identificador único del propietario (GUID).</param>
        /// <response code="204">Propietario eliminado exitosamente.</response>
        /// <response code="404">Si no se encuentra el propietario con el id especificado.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new CommandDeleteOwner { Id = id };

            await mediator.Send(command);
            return NoContent();
        }
    }
}
