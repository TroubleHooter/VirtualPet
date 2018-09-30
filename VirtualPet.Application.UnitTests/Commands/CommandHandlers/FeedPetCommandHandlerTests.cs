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
    public class FeedPetCommandHandlerTests
    {
        private readonly Mock<VirtualPetDbContext> context;

        public FeedPetCommandHandlerTests()
        {
            context = new Mock<VirtualPetDbContext>();
        }

        [Fact]
        public void Feed_Pet_Events_Created_Pet_Updated_Success_Returned()
        {
            //Arrange
            var now = DateTime.Now;
            var lastUpdated = now.AddMinutes(-10);

            var pet = new Pet
            {
                Name = "Fido",
                PetTypeId = (int)PetTypes.Dog,
                LastUpdated = lastUpdated,
                PetProfileId = 1,
                Profile = new PetProfile(),
                UserId = 1, Events = new List<Event>()
            };

            var options = new DbContextOptionsBuilder<VirtualPetDbContext>()
                .UseInMemoryDatabase(databaseName: "Update_Pet_Create_Feed_Event")
                .Options;


            using (var inMemoryContext = new VirtualPetDbContext(options))
            {
                inMemoryContext.Pets.Add(pet);
                inMemoryContext.SaveChanges();
                var sut = new FeedPetCommandHandler(inMemoryContext);

                //Act
                var result = sut.Handle(new FeedPetCommand(pet.Id, now), CancellationToken.None);

                //Assert
                Assert.Equal(ResultType.Success, result.Result.ResultType);
                Assert.True(inMemoryContext.Events.Any());
                Assert.Equal((int)EventTypes.Fed, inMemoryContext.Events.First().EventTypeId);
                Assert.Equal(now, inMemoryContext.Pets.First().LastUpdated);

            }
        }
        [Fact]
        public void Get_Owned_Pets_And_Returns_NotFoundFor_Invalid_PetId()
        {
            //Arrange
            var sut = new FeedPetCommandHandler(context.Object);
            var now = DateTime.Now;
            var events = new List<Event>().AsQueryable();
            var pets = new List<Pet>().AsQueryable();

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
            context.Setup(c => c.Events).Returns(mockEventSet.Object);

            //Act
            var result = sut.Handle(new FeedPetCommand(1, now), CancellationToken.None);

            //Assert
            Assert.True(result.Result.ResultType == ResultType.NotFound);
        }
    }
}
