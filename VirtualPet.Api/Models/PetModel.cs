namespace VirtualPet.Api.Dtos
{
    public class PetModel : BaseModel
    {
        public string Name { get; set; }
        public int Mood { get; set; }
        public int Hunger { get; set; }
        public int HungerTimeMultiplier { get; set; }
        public int MoodTimeMultiplier { get; set; }
        public string PetType { get; set; }
    }
}
