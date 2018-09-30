using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using EfCore.InMemoryHelpers;
using Microsoft.EntityFrameworkCore;
using Moq;
using VirtualPet.Application.Commands;
using VirtualPet.Application.Commands.CommandHandlers;
using VirtualPet.Application.Dtos;
using VirtualPet.Application.Entities;
using VirtualPet.Application.Enums;
using VirtualPet.Application.HandlerResponse;
using VirtualPet.Application.Mappers;
using VirtualPet.Application.ValueObjects;
using Xunit;

namespace VirtualPet.Application.UnitTests.Commands.CommandHandlers
{
    public class CreatePetCommandHandlerTests
    {
        private readonly Mock<VirtualPetDbContext> context;
        private readonly Mock<IMapper<Pet, PetDto>> mapper;

        public CreatePetCommandHandlerTests()
        {
            context = new Mock<VirtualPetDbContext>();
            mapper = new Mock<IMapper<Pet, PetDto>>();
        }

        [Fact]
        public void Create_Pet_Events_Created_Pet_Success_Returned()
        {
            //Arrange
            var now = DateTime.Now;
            var name = "Fido";
            var petType = (int) PetTypes.Dog;
            var profileId = 1;

            var options = new DbContextOptionsBuilder<VirtualPetDbContext>()
                .UseInMemoryDatabase(databaseName: "Create_Pet_Create_Born_Event")
                .Options;


            using (var inMemoryContext = new VirtualPetDbContext(options))
            {
                var sut = new CreatePetCommandHandler(inMemoryContext, mapper.Object);

                var newPet = new PetDto {CreateDate = now, PetTypeId = petType, ProfileId = profileId, Name = name};

                //Act
                var result = sut.Handle(new CreatePetCommand(newPet), CancellationToken.None);

                //Assert
                Assert.Equal(ResultType.Success, result.Result.ResultType);
                Assert.True(inMemoryContext.Events.Any());
                Assert.Equal((int) EventTypes.Born, inMemoryContext.Events.First().EventTypeId);
                Assert.Equal(now, inMemoryContext.Pets.First().CreateDate);
                Assert.Equal(name, inMemoryContext.Pets.First().Name);
                Assert.Equal(petType, inMemoryContext.Pets.First().PetTypeId);
            }
        }

        //TODO test the exception being thrown for ForeignKey violations as the EF core Inmemory databases does not support relational checking i.e
        //TODO https://github.com/aspnet/EntityFrameworkCore/issues/2166 and the Nuget package here does not work with latest EF 2.1.3


        //[Fact]
        //public async void Create_Pet_Returns_Exception_For_PetType_NotFound()
        //{
        //    //Arrange
        //    var now = DateTime.Now;
        //    var name = "Fido";
        //    var petType = 10;
        //    var profileId = 1;

        //    var options = new DbContextOptionsBuilder<VirtualPetDbContext>()
        //        .UseInMemoryDatabase(databaseName: "Create_Pet_Exception_Thrown_No_PetType")
        //        .Options;

        //    var newPetType = new PetType{ CreateDate = now, Name = "Name"};

        //  //  var builder = new DbContextOptionsBuilder<VirtualPetDbContext>();
        //    using (var inMemoryContext = InMemoryContextBuilder.Build<VirtualPetDbContext>())
        //    {
        //        //inMemoryContext.PetTypes.Add(new PetType {CreateDate = now, Name = "Name"});

        //        //inMemoryContext.SaveChanges();



        //        var sut = new CreatePetCommandHandler(inMemoryContext, mapper.Object);

        //        var newPet = new PetDto {CreateDate = now, PetTypeId = petType, ProfileId = profileId, Name = name};

        //        //Act
        //       // var result = sut.Handle(new CreatePetCommand(newPet), CancellationToken.None);

        //        //Assert
        //        await Assert.ThrowsAsync<ArgumentException>(() => sut.Handle(new CreatePetCommand(newPet), CancellationToken.None));
        //    }
        //}
    }
}
