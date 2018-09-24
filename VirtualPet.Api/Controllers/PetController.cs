using Microsoft.AspNetCore.Mvc;
using VirtualPet.Api.Dtos;
using VirtualPet.Data.Repositories;

namespace VirtualPet.Api.Controllers
{
    [Route("virtual-pet/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private PetRepository _repository;

        public PetController(PetRepository repository)
        {
            _repository = repository;
        }

        // GET virtual-pet/pet/
        [HttpGet()]
        public ActionResult<string> Get()
        {
            return "Welcome to Virtual pet";
        }
        // GET virtual-pet/pet/petId
        [HttpGet("{petId}")]
        public ActionResult<PetModel> Get(int petId)
        {
           var result = _repository.GetById(petId);

            if (result == null)
                return NotFound();

            return new PetModel();
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
