using System.Collections.Generic;

namespace VirtualPet.Application.Entities
{
    public class User : Entity
    {
        public List<Pet> Pets { get; set; }
    }
}
