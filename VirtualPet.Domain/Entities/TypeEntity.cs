namespace VirtualPet.Domain.Entities
{
    public abstract class TypeEntity: Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
