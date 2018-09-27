using System;
using VirtualPet.Application.Entities;

namespace VirtualPet.Application.ValueObjects
{
    public class Event : ValueObject<Event>
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public int PetId { get; set; }
        public int EventTypeId { get; set; }

        protected override bool EqualsCore(Event other)
        {
            return (Id == other.Id &&
                    PetId == other.PetId &&
                    EventTypeId == other.EventTypeId &&
                    CreateDate == other.CreateDate);
        }

        protected override int GetHashCodeCore()
        {
            var hashCode = Id;

            hashCode = (hashCode * 376) ^ PetId;
            hashCode = (hashCode * 376) ^ EventTypeId;
            hashCode = (hashCode * 376) ^ CreateDate.GetHashCode();

            return hashCode;
        }
    }
}
