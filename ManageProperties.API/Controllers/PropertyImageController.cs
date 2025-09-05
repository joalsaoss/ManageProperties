using ManageProperties.API.DTOs.PropertyImages;
using ManagerProperties.Application.UseCases.PropertyImages.Commands.CreatePropertyImage;
using ManagerProperties.Application.UseCases.PropertyImages.Queries.GetAllPropertyImage;
using ManagerProperties.Application.UseCases.PropertyImages.Queries.GetPropertyImageDetail;
using ManagerProperties.Application.Utilities.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace ManageProperties.API.Controllers
{
    [ApiController]
    [Route("api/propertyimage")]
    public class PropertyImageController:ControllerBase
    {
        private readonly IMediator mediator;

        public PropertyImageController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyImageDetailDTO>> Get(Guid id)
        {
            var query = new GetPropertyImageDetail { Id = id};
            var result = await mediator.Send(query);
            return result;
        }
        

        [HttpGet]
        public async Task<ActionResult<List<GetAllPropertyImageDTO>>> GetAll()
        {
            var query = new GetAllPropertyImage();
            var result = await mediator.Send(query);
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreatePropertyImageDTO createPropertyImageDTO)
        {
            var command = new CommandCreatePropertyImage
            {
                PropertyId = createPropertyImageDTO.PropertyId,
                Image = createPropertyImageDTO.Image,
                Enable = createPropertyImageDTO.Enable
            };
            await mediator.Send(command);
            return Ok();
        }
    }
}
