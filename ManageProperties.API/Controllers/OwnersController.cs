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
        public async Task<IActionResult> Post(CreateOwnerDTO createOwnerDTO)
        {
            var command = new CommandCreateOwner
            {
                Name = createOwnerDTO.Name,
                Address = createOwnerDTO.Address,
                Photo = createOwnerDTO.Photo,
                Birthday = createOwnerDTO.Birthday
            };

            await mediator.Send(command);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateOwnerDTO ownerDTO)
        {
            var command = new CommandUpdateOwner { id = id, 
                Name = ownerDTO.Name, 
                Address = ownerDTO.Address, 
                Photo = ownerDTO.Photo, 
                Birthday = ownerDTO.Birthday };

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
