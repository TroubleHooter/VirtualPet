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
    public class GetPetQueryHandler : IRequestHandler<GetPetQuery, HandlerResponse<PetDto>>
    {
        private VirtualPetDbContext context;
        private IMapper<Pet, PetDto> mapper;

        public GetPetQueryHandler(VirtualPetDbContext context, IMapper<Pet, PetDto> mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public Task<HandlerResponse<PetDto>> Handle(GetPetQuery request, CancellationToken cancellationToken)
        {
            var dbResult = context.Pets.Include(p => p.Profile).Include(p => p.TypeOfPet)
                .FirstOrDefault(p => p.Id == request.PetId);

            if(dbResult == null)
                 return Task.FromResult(new HandlerResponse<PetDto>(ResultType.NotFound, null));

            dbResult.UpDatePet(request.UpdateDateTime);

            return Task.FromResult(new HandlerResponse<PetDto>(ResultType.Success, mapper.Map(dbResult)));
        }
    }
}
