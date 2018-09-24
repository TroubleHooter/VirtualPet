namespace VirtualPet.Domain.Entities
{
    public class Event : Entity
    {
        public EventType TypeOfEvent { get; set; }
        public Pet PetEvent { get; set; }
    }
}
