using System.Collections.Generic;
using MediatR;
using VirtualPet.Application.Dtos;
using VirtualPet.Application.HandlerResponse;

namespace VirtualPet.Application.Queries
{
    public class GetOwnedPetsQuery : IRequest<HandlerResponse<List<PetDto>>>
    {
        public GetOwnedPetsQuery(int ownerId)
        {
            OwnerId = ownerId;
        }
        public int OwnerId { get; set; }
    }
}
