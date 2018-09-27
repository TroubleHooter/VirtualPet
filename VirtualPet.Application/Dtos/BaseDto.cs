using System;

namespace VirtualPet.Application.Dtos
{
    public abstract class BaseDto
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
