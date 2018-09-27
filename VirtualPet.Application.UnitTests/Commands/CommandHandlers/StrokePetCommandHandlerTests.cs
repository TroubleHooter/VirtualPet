using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Moq;
using VirtualPet.Application.Dtos;
using VirtualPet.Application.Entities;
using VirtualPet.Application.Enums;
using VirtualPet.Application.HandlerResponse;
using VirtualPet.Application.Mappers;
using VirtualPet.Application.Queries;
using VirtualPet.Application.Queries.QueryHandlers;
using Xunit;

namespace VirtualPet.Application.UnitTests.Commands.CommandHandlers
{
    public class StrokePetCommandHandlerTests
    {
        private readonly GetPetQueryHandler sut;
        private readonly Mock<VirtualPetDbContext> context;
        private readonly Mock<IMapper<Pet, PetDto>> mapper;

        public StrokePetCommandHandlerTests()
        {
            mapper = new Mock<IMapper<Pet, PetDto>>();
            context = new Mock<VirtualPetDbContext>();
            sut = new GetPetQueryHandler(context.Object, mapper.Object);
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

            var mockPetSet = new Mock<DbSet<Pet>>();

            mockPetSet.As<IQueryable<Pet>>().Setup(m => m.Provider).Returns(pets.Provider);
            mockPetSet.As<IQueryable<Pet>>().Setup(m => m.Expression).Returns(pets.Expression);
            mockPetSet.As<IQueryable<Pet>>().Setup(m => m.ElementType).Returns(pets.ElementType);
            mockPetSet.As<IQueryable<Pet>>().Setup(m => m.GetEnumerator()).Returns(pets.GetEnumerator());

            context.Setup(c => c.Pets).Returns(mockPetSet.Object);
            mapper.Setup(m => m.Map(pets.First())).Returns(new PetDto());

            //Act
            var result =  sut.Handle(new GetPetQuery(1, now), CancellationToken.None);

            //Assert
            Assert.True(result.Result.ResultType == ResultType.Success);

        }
        [Fact]
        public void Get_Owned_Pets_And_Returns_NotFound()
        {
            //Arrange
            var now = DateTime.Now;

            IQueryable<Pet> pets = new List<Pet>().AsQueryable();

            var mockPetSet = new Mock<DbSet<Pet>>();

            mockPetSet.As<IQueryable<Pet>>().Setup(m => m.Provider).Returns(pets.Provider);
            mockPetSet.As<IQueryable<Pet>>().Setup(m => m.Expression).Returns(pets.Expression);
            mockPetSet.As<IQueryable<Pet>>().Setup(m => m.ElementType).Returns(pets.ElementType);
            mockPetSet.As<IQueryable<Pet>>().Setup(m => m.GetEnumerator()).Returns(pets.GetEnumerator());

            context.Setup(c => c.Pets).Returns(mockPetSet.Object);

            //Act
            var result = sut.Handle(new GetPetQuery(1, now), CancellationToken.None);

            //Assert
            Assert.True(result.Result.ResultType == ResultType.NotFound);
        }
    }
}
