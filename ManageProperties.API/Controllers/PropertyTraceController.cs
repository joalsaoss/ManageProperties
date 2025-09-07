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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new CommandDeletePropertyTrace { Id = id };
            await mediator.Send(command);
            return Ok();
        }
    }
}
