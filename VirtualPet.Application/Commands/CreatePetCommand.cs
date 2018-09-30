using System;
using MediatR;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;
using VirtualPet.Application.Dtos;
using VirtualPet.Application.HandlerResponse;

namespace VirtualPet.Application.Commands
{
    public class CreatePetCommand : IRequest<HandlerResponse<PetDto>>
    {
        public string Name { get; set; }
        public int ProfileId { get; set; }
        public int TypeOfPetId { get; set; }
        public DateTime CreateDate { get; set; }
        public int OwnerId { get; set; }

        public CreatePetCommand(PetDto pet)
        {
            Name = pet.Name;
            ProfileId = pet.ProfileId;
            TypeOfPetId = pet.PetTypeId;
            CreateDate = pet.CreateDate;
            OwnerId = pet.OwnerId;
        }
    }
}
