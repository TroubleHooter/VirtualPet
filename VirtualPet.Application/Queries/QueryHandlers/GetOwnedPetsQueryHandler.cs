
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VirtualPet.Application.Dtos;
using VirtualPet.Application.Entities;
using VirtualPet.Application.HandlerResponse;
using VirtualPet.Application.Mappers;

namespace VirtualPet.Application.Queries.QueryHandlers
{
    public class GetOwnedPetsQueryHandler : IRequestHandler<GetOwnedPetsQuery, HandlerResponse<List<PetDto>>>
    {
        private VirtualPetDbContext context;
        private IMapper<Pet, PetDto> mapper;
        public GetOwnedPetsQueryHandler(VirtualPetDbContext context, IMapper<Pet, PetDto> mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public Task<HandlerResponse<List<PetDto>>> Handle(GetOwnedPetsQuery request, CancellationToken cancellationToken)
        {
            var dbResult = context.Pets.Include(p => p.Profile).Include(p => p.TypeOfPet)
                .Where(p => p.UserId == request.OwnerId);

            if(!dbResult.Any())
                 return Task.FromResult(new HandlerResponse<List<PetDto>>(ResultType.NotFound, null));

            var results = new List<PetDto>();

            foreach (var pet in dbResult)
            {
                pet.UpDatePet(request.UpdateDateTime);
                results.Add(mapper.Map(pet));
            }

            return Task.FromResult(new HandlerResponse<List<PetDto>>(ResultType.Success, results));
        }
    }
}
