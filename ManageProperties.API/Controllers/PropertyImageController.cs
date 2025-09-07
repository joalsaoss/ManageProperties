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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new CommandDeletePropertyTrace { Id = id };
            await mediator.Send(command);
            return Ok();
        }
    }
}
