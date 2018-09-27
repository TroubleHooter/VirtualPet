using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VirtualPet.Application.Enums;
using VirtualPet.Application.ValueObjects;

namespace VirtualPet.Application.Commands.CommandHandlers
{
    public class StrokePetCommandHandler : IRequestHandler<StrokePetCommand>
    {
        private VirtualPetDbContext context;
        public StrokePetCommandHandler(VirtualPetDbContext context)
        {
            this.context = context;
        }

        public Task<Unit> Handle(StrokePetCommand request, CancellationToken cancellationToken)
        {
            var result = context.Pets.Include(p => p.Profile)
                .Single(p => p.Id == request.PetId);

            result.Stroke(request.UpdateDateTime);

            //Add new Event
            context.Events.Add(new Event
                {
                    CreateDate = request.UpdateDateTime,
                    PetId = result.Id,
                    EventTypeId = (int)EventTypes.Stroked
                });

            context.SaveChanges();

            return Task.FromResult(new Unit());
        }
    }
}
