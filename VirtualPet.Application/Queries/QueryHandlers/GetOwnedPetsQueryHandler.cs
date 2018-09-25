
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using VirtualPet.Application.Dtos;
using VirtualPet.Application.HandlerResponse;

namespace VirtualPet.Application.Queries.QueryHandlers
{
    public class GetOwnedPetsQueryHandler : IRequestHandler<GetOwnedPetsQuery, HandlerResponse<List<PetDto>>>
    {
        private VirtualPetDbContext context;
        private IMapper mapper;
        public GetOwnedPetsQueryHandler(VirtualPetDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public Task<HandlerResponse<List<PetDto>>> Handle(GetOwnedPetsQuery request, CancellationToken cancellationToken)
        {
            var result = context.Pets.Where(p => p.UserId == request.OwnerId);

            var mappedDto = mapper.Map<List<PetDto>>(result);

            return Task.FromResult(new HandlerResponse<List<PetDto>>(ResultType.Success, mappedDto));
        }
    }
}
