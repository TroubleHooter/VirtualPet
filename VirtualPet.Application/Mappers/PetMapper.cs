using System;
using VirtualPet.Application.Dtos;
using VirtualPet.Application.Entities;

namespace VirtualPet.Application.Mappers
{
    public class PetMapper : IMapper<Pet, PetDto>
    {
        public PetDto Map(Pet pet)
        {
            var dto = new PetDto
            {
                Name = pet.Name,
                Hunger = pet.Hunger,
                Mood = pet.Mood,
                HungerTimeMultiplier = pet.Profile.HungerTimeModifier,
                MoodTimeMultiplier = pet.Profile.MoodTimeModifier,
                PetType = pet.TypeOfPet.Name,
                Id = pet.Id,
                CreateDate = pet.CreateDate
            };

            return dto;
        }
    }
}
