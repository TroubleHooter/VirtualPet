using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VirtualPet.Application.Enums;
using VirtualPet.Application.HandlerResponse;
using VirtualPet.Application.ValueObjects;

namespace VirtualPet.Application.Commands.CommandHandlers
{
    public class FeedPetCommandHandler : IRequestHandler<FeedPetCommand, HandlerResponse<string>>
    {
        private VirtualPetDbContext context;
        public FeedPetCommandHandler(VirtualPetDbContext context)
        {
            this.context = context;
        }

        public Task<HandlerResponse<string>> Handle(FeedPetCommand request, CancellationToken cancellationToken)
        {
            var result = context.Pets.Include(p => p.Profile).
                SingleOrDefault(p => p.Id == request.PetId);

            if(result == null)
                return Task.FromResult(new HandlerResponse<string>(ResultType.NotFound, string.Format("Pet with an id of {0} was not found", request.PetId)));

            result.Feed(request.UpdateDateTime);

            //Add new Event
            context.Events.Add(new Event
                {
                    CreateDate = request.UpdateDateTime,
                    PetId = result.Id,
                    EventTypeId = (int)EventTypes.Fed
                });

            context.SaveChanges();

            return Task.FromResult(new HandlerResponse<string>(ResultType.Success));
        }
    }
}
