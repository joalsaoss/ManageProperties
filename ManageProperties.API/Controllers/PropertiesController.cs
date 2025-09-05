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
    [ApiController]
    [Route("api/properties")]
    public class PropertiesController: ControllerBase
    {
        private readonly IMediator mediator;

        public PropertiesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetAllPropertiesDTO>>> GetAll()
        {
            var query = new GetAllProperties();
            var result = await mediator.Send(query);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyDetailDTO>> Get(Guid id)
        {
            var query = new GetPropertyDetail() { Id = id};
            var result = await mediator.Send(query);
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreatePropertiesDTO createPropertiesDTO)
        {
            var command = new CommandCreateProperty
            {
                OwnerId = createPropertiesDTO.OwnerId,
                CodeInternal = createPropertiesDTO.CodeInternal,
                Name = createPropertiesDTO.Name,
                Address = createPropertiesDTO.Address,
                Price = createPropertiesDTO.Price,
                Year = createPropertiesDTO.Year
            };
            await mediator.Send(command);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdatePropertyDTO updatePropertyDTO)
        {
            var command = new CommandUpdateProperty
            {
                Id = id,
                OwnerId = updatePropertyDTO.OwnerId,
                CodeInternal = updatePropertyDTO.CodeInternal,
                Name = updatePropertyDTO.Name,
                Address = updatePropertyDTO.Address,
                Price = updatePropertyDTO.Price,
                Year = updatePropertyDTO.Year
            };
            await mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new CommandDeleteProperty { Id = id };
            await mediator.Send(command);
            return Ok();
        }
    }
}
