using MediatR;
using Microsoft.AspNetCore.Mvc;
using VirtualPet.Application.Dtos;
using VirtualPet.Application.Queries;

namespace VirtualPet.Api.Controllers
{
    [Route("virtual-pet/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IMediator mediator;

        public PetController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET virtual-pet/pet/
        [HttpGet()]
        public ActionResult<string> Get()
        {
            return "Welcome to Virtual pet";
        }
        // GET virtual-pet/pet/petId
        [HttpGet("{petId}")]
        public ActionResult<PetDto> Get(int petId)
        {
            var result = mediator.Send(new GetOwnedPetsQuery(1));

            if (result == null)
                return NotFound();

            return new PetDto();
        }
        // GET virtual-pet/pet/ownerId
        [HttpGet("getall/{userId}")]
        public ActionResult<string> GetAll(int ownerId)
        {
            return "value";
        }
        // GET virtual-pet/pet/stroke/petId
        [HttpGet("stroke/{petId}")]
        public ActionResult<string> Stroke(int petId)
        {
            return "value";
        }

        // GET virtual-pet/pet/feed/petId
        [HttpGet("feed/{petId}")]
        public ActionResult<string> Feed(int petId)
        {
            return "value";
        }

        // POST virtual-pet/pet/
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

    }
}
