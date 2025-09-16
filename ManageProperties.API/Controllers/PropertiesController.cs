using ManageProperties.API.DTOs.Properties;
using ManagerProperties.Application.UseCases.Properties.Commands.CreateProperty;
using ManagerProperties.Application.UseCases.Properties.Commands.DeleteProperty;
using ManagerProperties.Application.UseCases.Properties.Commands.UpdateProperty;
using ManagerProperties.Application.UseCases.Properties.Queries.GetAllProperties;
using ManagerProperties.Application.UseCases.Properties.Queries.GetPropertyDetail;
using ManagerProperties.Application.Utilities.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace ManageProperties.API.Controllers
{
    /// <summary>
    /// Controlador para gestionar las propiedades.
    /// </summary>
    [ApiController]
    [Route("api/properties")]
    public class PropertiesController: ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Inicializa una nueva instancia del <see cref="PropertiesController"/>.
        /// </summary>
        /// <param name="mediator">Instancia de <see cref="IMediator"/> para manejar comandos y consultas.</param>
        public PropertiesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Obtiene la lista de todas las propiedades registradas.
        /// </summary>
        /// <returns>Una lista de <see cref="GetAllPropertiesDTO"/>.</returns>
        /// <response code="200">Devuelve la lista de propiedades.</response>
        [HttpGet]
        public async Task<ActionResult<List<GetAllPropertiesDTO>>> GetAll()
        {
            var query = new GetAllProperties();
            var result = await mediator.Send(query);
            return result;
        }

        /// <summary>
        /// Obtiene el detalle de una propiedad específica.
        /// </summary>
        /// <param name="id">Identificador único de la propiedad (GUID).</param>
        /// <returns>Un objeto <see cref="PropertyDetailDTO"/> con la información detallada de la propiedad.</returns>
        /// <response code="200">Devuelve la propiedad encontrada.</response>
        /// <response code="404">Si no se encuentra la propiedad con el id especificado.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyDetailDTO>> Get(Guid id)
        {
            var query = new GetPropertyDetail() { Id = id};
            var result = await mediator.Send(query);
            return result;
        }

        /// <summary>
        /// Crea una nueva propiedad.
        /// </summary>
        /// <param name="request">Objeto <see cref="CreatePropertiesDTO"/> con los datos de la propiedad a crear.</param>
        /// <response code="200">Propiedad creada exitosamente.</response>
        /// <response code="400">Si los datos enviados no son válidos.</response>
        [HttpPost]
        public async Task<IActionResult> Post(CreatePropertiesDTO request)
        {
            var command = new CommandCreateProperty
            {
                OwnerId = request.OwnerId,
                CodeInternal = request.CodeInternal,
                Name = request.Name,
                Address = request.Address,
                Price = request.Price,
                Year = request.Year
            };
            await mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Actualiza la información de una propiedad existente.
        /// </summary>
        /// <param name="id">Identificador único de la propiedad (GUID).</param>
        /// <param name="request">Objeto <see cref="UpdatePropertyDTO"/> con los datos actualizados de la propiedad.</param>
        /// <response code="200">Propiedad actualizada exitosamente.</response>
        /// <response code="400">Si los datos enviados no son válidos.</response>
        /// <response code="404">Si no se encuentra la propiedad a actualizar.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdatePropertyDTO request)
        {
            var command = new CommandUpdateProperty
            {
                Id = id,
                OwnerId = request.OwnerId,
                CodeInternal = request.CodeInternal,
                Name = request.Name,
                Address = request.Address,
                Price = request.Price,
                Year = request.Year
            };
            await mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Elimina una propiedad específica.
        /// </summary>
        /// <param name="id">Identificador único de la propiedad (GUID).</param>
        /// <response code="200">Propiedad eliminada exitosamente.</response>
        /// <response code="404">Si no se encuentra la propiedad con el id especificado.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new CommandDeleteProperty { Id = id };
            await mediator.Send(command);
            return Ok();
        }
    }
}
