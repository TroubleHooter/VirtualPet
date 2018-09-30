namespace VirtualPet.Application.Entities
{
    public class Event : Entity
    {
        public int PetId { get; set; }
        public int EventTypeId { get; set; }
    }
}
