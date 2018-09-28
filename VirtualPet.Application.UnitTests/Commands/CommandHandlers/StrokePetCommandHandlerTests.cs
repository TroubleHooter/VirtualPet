using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Moq;
using VirtualPet.Application.Commands;
using VirtualPet.Application.Commands.CommandHandlers;
using VirtualPet.Application.Entities;
using VirtualPet.Application.Enums;
using VirtualPet.Application.HandlerResponse;
using VirtualPet.Application.ValueObjects;
using Xunit;

namespace VirtualPet.Application.UnitTests.Commands.CommandHandlers
{
    public class StrokePetCommandHandlerTests
    {
        private readonly StrokePetCommandHandler sut;
        private readonly Mock<VirtualPetDbContext> context;

        public StrokePetCommandHandlerTests()
        {
            context = new Mock<VirtualPetDbContext>();
            sut = new StrokePetCommandHandler(context.Object);
        }

        [Fact]
        public void Stroke_Pet_And_Success_Returned()
        {
            //Arrange
            var now = DateTime.Now;
            var lastUpdated = now.AddMinutes(-10);

            IQueryable<Pet> pets = new List<Pet>
            {
                new Pet
                {
                    Id = 1,
                    Name = "Fido",
                    CreateDate = now,
                    PetTypeId = (int) PetTypes.Dog,
                    LastUpdated = lastUpdated,
                    PetProfileId = 1,
                    Profile = new PetProfile(),
                    UserId = 1
                },
            }.AsQueryable();

            SetupDbSets(new List<Event>().AsQueryable(), pets);

            //Act
            var result =  sut.Handle(new StrokePetCommand(1, now), CancellationToken.None);

            //Assert
            Assert.True(result.Result.ResultType == ResultType.Success);
          //  var t = context.Object.Events.Single(e => e.EventTypeId == (int) EventTypes.Fed);

        }
        [Fact]
        public void Get_Owned_Pets_And_Returns_NotFound()
        {
            //Arrange
            var now = DateTime.Now;
            SetupDbSets(new List<Event>().AsQueryable(), new List<Pet>().AsQueryable());

            //Act
            var result = sut.Handle(new StrokePetCommand(1, now), CancellationToken.None);

            //Assert
            Assert.True(result.Result.ResultType == ResultType.NotFound);
        }

        private void SetupDbSets(IQueryable<Event> events, IQueryable<Pet> pets)
        {
            var mockEventSet = new Mock<DbSet<Event>>();
            mockEventSet.As<IQueryable<Event>>().Setup(m => m.Provider).Returns(events.Provider);
            mockEventSet.As<IQueryable<Event>>().Setup(m => m.Expression).Returns(events.Expression);
            mockEventSet.As<IQueryable<Event>>().Setup(m => m.ElementType).Returns(events.ElementType);
            mockEventSet.As<IQueryable<Event>>().Setup(m => m.GetEnumerator()).Returns(events.GetEnumerator());

            var mockPetSet = new Mock<DbSet<Pet>>();
            mockPetSet.As<IQueryable<Pet>>().Setup(m => m.Provider).Returns(pets.Provider);
            mockPetSet.As<IQueryable<Pet>>().Setup(m => m.Expression).Returns(pets.Expression);
            mockPetSet.As<IQueryable<Pet>>().Setup(m => m.ElementType).Returns(pets.ElementType);
            mockPetSet.As<IQueryable<Pet>>().Setup(m => m.GetEnumerator()).Returns(pets.GetEnumerator());

            context.Setup(c => c.Pets).Returns(mockPetSet.Object);
           // context.Object.Events = new DbSet<Event>();
             context.Setup(c => c.Events).Returns(mockEventSet.Object);
        }
    }
}
