using System;
using System.Collections.Generic;
using VirtualPet.Application.Dtos;
using VirtualPet.Application.Entities;

namespace VirtualPet.Application.Mappers
{
    public class PetsMapper : IMapper<List<Pet>, List<PetDto>>
    {
        public List<PetDto> Map(List<Pet> pets)
        {
            var returnList = new List<PetDto>();
            foreach (var pet in pets)
            {
                returnList.Add(new PetDto
                    {
                        Name = pet.Name,
                        Hunger = pet.Hunger,
                        Mood = pet.Mood,
                        HungerTimeMultiplier = pet.Profile.HungerTimeModifier,
                        MoodTimeMultiplier = pet.Profile.MoodTimeModifier,
                        PetType = pet.TypeOfPet.Name,
                        Id = pet.Id,
                        CreateDate = pet.CreateDate
                    }
                );
            }

            return returnList;
        }
    }
}
