using System.Collections.Generic;

namespace VirtualPet.Domain.Entities
{
    public class User : Entity
    {
        public List<Pet> Pets { get; set; }
    }
}
