
using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VirtualPet.Application.Commands;
using VirtualPet.Application.Dtos;
using VirtualPet.Application.HandlerResponse;
using VirtualPet.Application.Queries;

namespace VirtualPet.Api.Controllers
{
    [Route("virtual-pet/[controller]")]
    [ApiController]
    public class PetController : BaseApiController
    {
        private readonly IMediator mediator;

        public PetController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET virtual-pet/pet/
        [HttpGet()]
        public IActionResult Get()
        {
            return ReturnResponse("Welcome to Virtual pet");
        }
        // GET virtual-pet/pet/petId
        [HttpGet("{petId}")]
        public async Task<IActionResult> Get(int petId)
        {
            var result = await mediator.Send(new GetPetQuery(petId, DateTime.Now));

            return ReturnResponse(result.Result);
        }
        // GET virtual-pet/pets/ownerId
        [HttpGet("pets/{OwnerId}")]
        public async Task<IActionResult> GetPets(int ownerId)
        {
            var result = await mediator.Send(new GetOwnedPetsQuery(ownerId, DateTime.Now));

            return ReturnResponse(result.Result);
        }
        // POST virtual-pet/pet/stroke/petId
        [HttpPost("stroke/{petId}")]
        public async Task<IActionResult> Stroke(int petId)
        {
           var result = await mediator.Send(new StrokePetCommand(petId, DateTime.Now));

            if (result.ResultType == ResultType.NotFound)
                return NotFound();

            return NoContent();
        }

        // POST virtual-pet/pet/feed/petId
        [HttpPost("feed/{petId}")]
        public async Task<IActionResult> Feed(int petId)
        {
            var result = await mediator.Send(new FeedPetCommand(petId, DateTime.Now));

            if (result.ResultType == ResultType.NotFound)
                return NotFound();

            return NoContent();
        }

        // POST virtual-pet/pet/
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PetDto pet)
        {
            pet.CreateDate = DateTime.Now;
            var result = await mediator.Send(new CreatePetCommand(pet));


            return ReturnResponse(result.Result);
        }

    }
}
