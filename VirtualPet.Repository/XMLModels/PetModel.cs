using System;

namespace VirtualPet.Repository.XMLModels
{
    public class PetModel: XmlModel
    {
        public string Name { get; set; }
        public int Mood { get; set; }
        public int Hunger { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
