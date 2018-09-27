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
        private IMapper<List<Pet>, List<PetDto>> mapper;

        public GetOwnedPetsQueryHandler(VirtualPetDbContext context, IMapper<List<Pet>, List<PetDto>> mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public Task<HandlerResponse<List<PetDto>>> Handle(GetOwnedPetsQuery request, CancellationToken cancellationToken)
        {
            var dbResult = context.Pets.Include(p => p.Profile).Include(p => p.TypeOfPet)
                .Where(p => p.UserId == request.OwnerId).ToList();

            if(!dbResult.Any())
                 return Task.FromResult(new HandlerResponse<List<PetDto>>(ResultType.NotFound, null));

            dbResult.ForEach(p => p.UpDatePet(request.UpdateDateTime));

            return Task.FromResult(new HandlerResponse<List<PetDto>>(ResultType.Success, mapper.Map(dbResult)));
        }
    }
}
