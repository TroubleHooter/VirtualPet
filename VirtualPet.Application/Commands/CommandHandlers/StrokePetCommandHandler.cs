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

            var strokeEvent = new Event
            {
                CreateDate = request.UpdateDateTime,
                PetId = result.Id,
                EventTypeId = (int) EventTypes.Stroked
            };

            context.Events.Add(strokeEvent);

            context.SaveChanges();

            return Task.FromResult(new Unit());
        }
    }
}
