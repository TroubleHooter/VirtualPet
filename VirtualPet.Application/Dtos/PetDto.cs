using System;
using System.Data;

namespace VirtualPet.Application.Dtos
{
    public class PetDto : BaseDto
    {
        public string Name { get; set; }
        public int Mood { get; set; }
        public int Hunger { get; set; }
        public int ProfileId { get; set; }
        public int HungerTimeMultiplier { get; set; }
        public int MoodTimeMultiplier { get; set; }
        public string PetType { get; set; }
        public int PetTypeId { get; set; }
        public int OwnerId { get; set; }
    }
}
