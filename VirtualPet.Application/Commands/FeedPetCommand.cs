using System;
using MediatR;
using VirtualPet.Application.HandlerResponse;

namespace VirtualPet.Application.Commands
{
    public class FeedPetCommand : IRequest<HandlerResponse<string>>
    {
        public int PetId { get; set; }
        public DateTime UpdateDateTime { get; set; }

        public FeedPetCommand(int petId, DateTime updateDateTime)
        {
            PetId = petId;
            UpdateDateTime = updateDateTime;
        }
    }
}
