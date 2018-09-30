using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VirtualPet.Application.Entities;
using VirtualPet.Application.Enums;
using VirtualPet.Application.HandlerResponse;

namespace VirtualPet.Application.Commands.CommandHandlers
{
    public class StrokePetCommandHandler : IRequestHandler<StrokePetCommand, HandlerResponse<string>>
    {
        private VirtualPetDbContext context;
        public StrokePetCommandHandler(VirtualPetDbContext context)
        {
            this.context = context;
        }

        public Task<HandlerResponse<string>> Handle(StrokePetCommand request, CancellationToken cancellationToken)
        {
            var result = context.Pets.Include(p => p.Profile).
                SingleOrDefault(p => p.Id == request.PetId);

            if(result == null)
                return Task.FromResult(new HandlerResponse<string>(ResultType.NotFound, string.Format("Pet with an id of {0} was not found", request.PetId)));

            result.Stroke(request.UpdateDateTime);

            //Add new Event
            context.Events.Add(new Event
                {
                    CreateDate = request.UpdateDateTime,
                    PetId = result.Id,
                    EventTypeId = (int)EventTypes.Stroked
                });

            context.SaveChanges();

            return Task.FromResult(new HandlerResponse<string>(ResultType.Success));
        }
    }
}
