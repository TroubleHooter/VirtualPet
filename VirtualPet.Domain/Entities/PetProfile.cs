namespace VirtualPet.Domain.Entities
{
    public class PetProfile : Entity
    {
        public int MoodTimeModifier { get; set; }
        public int HungerTimeModifier { get; set; }
        public int StrokeModifier { get; set; }
        public int FeedModifier { get; set; }
    }
}
