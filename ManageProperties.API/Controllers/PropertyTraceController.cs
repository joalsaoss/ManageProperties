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
    [ApiController]
    [Route("api/propertytrace")]
    public class PropertyTraceController:ControllerBase
    {
        private readonly IMediator mediator;

        public PropertyTraceController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyTraceDetailDTO>> Get(Guid id)
        {
            var query = new GetPropertyTraceDetail { Id = id };
            var result = await mediator.Send(query);
            return result;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetAllPropertyTracesDTO>>> GetAll()
        {
            var query = new GetAllPropertyTraces();
            var result = await mediator.Send(query);
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreatePropertyTraceDTO createPropertyTraceDTO)
        {
            var command = new CommandCreatePropertyTrace
            {
                PropertyId = createPropertyTraceDTO.PropertyId,
                DateSale = createPropertyTraceDTO.DateSale,
                Name = createPropertyTraceDTO.Name,
                Value = createPropertyTraceDTO.Value,
                Tax = createPropertyTraceDTO.Tax
            };
            await mediator.Send(command);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdatePropertyTraceDTO updatePropertyTraceDTO)
        {
            var command = new CommandUpdatePropertyTrace
            {
                Id = id,
                PropertyId = updatePropertyTraceDTO.PropertyId,
                DateSale = updatePropertyTraceDTO.DateSale,
                Name = updatePropertyTraceDTO.Name,
                Value = updatePropertyTraceDTO.Value,
                Tax = updatePropertyTraceDTO.Tax
            };

            await mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new CommandDeletePropertyTrace { Id = id };
            await mediator.Send(command);
            return Ok();
        }
    }
}
