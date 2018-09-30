using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VirtualPet.Application.Dtos;
using VirtualPet.Application.Entities;
using VirtualPet.Application.Enums;
using VirtualPet.Application.HandlerResponse;
using VirtualPet.Application.Mappers;

namespace VirtualPet.Application.Commands.CommandHandlers
{
    public class CreatePetCommandHandler : IRequestHandler<CreatePetCommand, HandlerResponse<PetDto>>
    {
        private VirtualPetDbContext context;
        private IMapper<Pet, PetDto> mapper;
        public CreatePetCommandHandler(VirtualPetDbContext context, IMapper<Pet, PetDto> mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public Task<HandlerResponse<PetDto>> Handle(CreatePetCommand request, CancellationToken cancellationToken)
        {
            var newPet = new Pet
            {
                CreateDate = request.CreateDate,
                LastUpdated = request.CreateDate,
                Name = request.Name,
                PetProfileId = request.ProfileId,
                PetTypeId = request.TypeOfPetId,
                UserId = request.OwnerId
            };

            context.Pets.Add(newPet);
            context.SaveChanges();

            var dbResult = context.Pets.Include(p => p.Profile).Include(p => p.TypeOfPet)
                .SingleOrDefault(p => p.Id == newPet.Id);

            //Add new Event
            context.Events.Add(new Event
                {
                    CreateDate = request.CreateDate,
                    PetId = newPet.Id,
                    EventTypeId = (int)EventTypes.Born
                });

            context.SaveChanges();

            var dto = mapper.Map(dbResult);

            return Task.FromResult(new HandlerResponse<PetDto>(ResultType.Success, dto));
        }
    }
}
