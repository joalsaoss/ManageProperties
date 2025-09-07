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
    [ApiController]
    [Route("api/owners")]
    public class OwnersController:ControllerBase
    {
        private readonly IMediator mediator;

        public OwnersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OwnerDetailDTO>> Get(Guid id)
        {
            var query = new GetOwnerDetail { Id = id};
            var result = await mediator.Send(query);
            return result;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetAllOwnersDTO>>> GetAll()
        {
            var query = new GetAllOwners();
            var result = await mediator.Send(query);
            return result;
        }

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new CommandDeleteOwner { Id = id };

            await mediator.Send(command);
            return NoContent();
        }
    }
}
