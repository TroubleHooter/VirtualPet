using MediatR;
using System;
using VirtualPet.Application.Dtos;
using VirtualPet.Application.HandlerResponse;

namespace VirtualPet.Application.Queries
{
    public class GetPetQuery : IRequest<HandlerResponse<PetDto>>
    {
        public int PetId { get; set; }
        public DateTime UpdateDateTime { get; set; }

        public GetPetQuery(int petId, DateTime updateDateTime)
        {
            PetId = petId;
            UpdateDateTime = updateDateTime;
        }
    }
}
