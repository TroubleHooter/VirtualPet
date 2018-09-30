using System.Collections.Generic;

namespace VirtualPet.Application.Entities
{
    public class PetType : TypeEntity
    {
        public List<Pet> Pets { get; set; }
    }
}
