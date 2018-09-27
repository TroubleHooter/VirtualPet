﻿
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
        public ActionResult<string> Get()
        {
            return "Welcome to Virtual pet";
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
        // GET virtual-pet/pet/stroke/petId
        [HttpPost("stroke/{petId}")]
        public async Task<IActionResult> Stroke(int petId)
        {
           var result = await mediator.Send(new StrokePetCommand(petId, DateTime.Now));

            if (result.ResultType == ResultType.NotFound)
                return NotFound();

            return NoContent();
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
