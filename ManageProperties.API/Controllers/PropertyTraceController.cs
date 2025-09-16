using ManageProperties.API.DTOs.PropertyTraces;
using ManagerProperties.Application.UseCases.PropertyTraces.Commands.CreatePropertyTrace;
using ManagerProperties.Application.UseCases.PropertyTraces.Commands.DeletePropertyTrace;
using ManagerProperties.Application.UseCases.PropertyTraces.Commands.UpdatePropertyTrace;
using ManagerProperties.Application.UseCases.PropertyTraces.Queries.GetAllPropertyTraces;
using ManagerProperties.Application.UseCases.PropertyTraces.Queries.GetPropertyTraceDetail;
using ManagerProperties.Application.Utilities.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace ManageProperties.API.Controllers
{
    /// <summary>
    /// Controlador para gestionar el seguimiento a las ventas de las propiedades.
    /// </summary>
    [ApiController]
    [Route("api/propertytrace")]
    public class PropertyTraceController:ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Inicializa una nueva instancia del <see cref="PropertyTraceController"/>.
        /// </summary>
        /// <param name="mediator">Instancia de <see cref="IMediator"/> para manejar comandos y consultas.</param>
        public PropertyTraceController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Obtiene el detalle de un registro de trazabilidad de una propiedad.
        /// </summary>
        /// <param name="id">Identificador único del registro (GUID).</param>
        /// <returns>Un objeto <see cref="PropertyTraceDetailDTO"/> con la información detallada del registro.</returns>
        /// <response code="200">Devuelve el registro encontrado.</response>
        /// <response code="404">Si no se encuentra el registro con el id especificado.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyTraceDetailDTO>> Get(Guid id)
        {
            var query = new GetPropertyTraceDetail { Id = id };
            var result = await mediator.Send(query);
            return result;
        }

        /// <summary>
        /// Obtiene la lista de todos los registros de trazabilidad de propiedades.
        /// </summary>
        /// <returns>Una lista de <see cref="GetAllPropertyTracesDTO"/>.</returns>
        /// <response code="200">Devuelve la lista de registros.</response>
        [HttpGet]
        public async Task<ActionResult<List<GetAllPropertyTracesDTO>>> GetAll()
        {
            var query = new GetAllPropertyTraces();
            var result = await mediator.Send(query);
            return result;
        }

        /// <summary>
        /// Crea un nuevo registro de trazabilidad para una propiedad.
        /// </summary>
        /// <param name="request">Objeto <see cref="CreatePropertyTraceDTO"/> con los datos del registro.</param>
        /// <response code="200">Registro creado exitosamente.</response>
        /// <response code="400">Si los datos enviados no son válidos.</response>
        [HttpPost]
        public async Task<IActionResult> Post(CreatePropertyTraceDTO request)
        {
            var command = new CommandCreatePropertyTrace
            {
                PropertyId = request.PropertyId,
                DateSale = request.DateSale,
                Name = request.Name,
                Value = request.Value,
                Tax = request.Tax
            };
            await mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Actualiza un registro de trazabilidad existente de una propiedad.
        /// </summary>
        /// <param name="id">Identificador único del registro (GUID).</param>
        /// <param name="request">Objeto <see cref="UpdatePropertyTraceDTO"/> con los datos actualizados del registro.</param>
        /// <response code="200">Registro actualizado exitosamente.</response>
        /// <response code="400">Si los datos enviados no son válidos.</response>
        /// <response code="404">Si no se encuentra el registro a actualizar.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdatePropertyTraceDTO request)
        {
            var command = new CommandUpdatePropertyTrace
            {
                Id = id,
                PropertyId = request.PropertyId,
                DateSale = request.DateSale,
                Name = request.Name,
                Value = request.Value,
                Tax = request.Tax
            };

            await mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Elimina un registro de trazabilidad específico.
        /// </summary>
        /// <param name="id">Identificador único del registro (GUID).</param>
        /// <response code="200">Registro eliminado exitosamente.</response>
        /// <response code="404">Si no se encuentra el registro con el id especificado.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new CommandDeletePropertyTrace { Id = id };
            await mediator.Send(command);
            return Ok();
        }
    }
}
