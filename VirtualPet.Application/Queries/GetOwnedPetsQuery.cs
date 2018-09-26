using System;
using System.Collections.Generic;
using MediatR;
using VirtualPet.Application.Dtos;
using VirtualPet.Application.HandlerResponse;

namespace VirtualPet.Application.Queries
{
    public class GetOwnedPetsQuery : IRequest<HandlerResponse<List<PetDto>>>
    {
        public int OwnerId { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public GetOwnedPetsQuery(int ownerId, DateTime updateDateTime)
        {
            OwnerId = ownerId;
            UpdateDateTime = updateDateTime;
        }
        
    }
}
