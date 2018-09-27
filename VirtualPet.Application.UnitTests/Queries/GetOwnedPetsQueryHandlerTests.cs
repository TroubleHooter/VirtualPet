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

namespace VirtualPet.Application.UnitTests.Queries
{
    public class GetOwnedPetsQueryHandlerTests
    {
        private readonly GetOwnedPetsQueryHandler sut;
        private readonly Mock<VirtualPetDbContext> context;
        private readonly Mock<IMapper<List<Pet>, List<PetDto>>> mapper;

        public GetOwnedPetsQueryHandlerTests()
        {
            mapper = new Mock<IMapper<List<Pet>, List<PetDto>>>();
            context = new Mock<VirtualPetDbContext>();
            sut = new GetOwnedPetsQueryHandler(context.Object, mapper.Object);
        }

        [Fact]
        public void Get_Owned_Pets_And_Returns_Success()
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
                new Pet
                {
                    Id = 2,
                    Name = "Mog",
                    CreateDate = now,
                    PetTypeId = (int) PetTypes.Cat,
                    LastUpdated = lastUpdated,
                    PetProfileId = 1,
                    Profile = new PetProfile(),
                    UserId = 1
                }

            }.AsQueryable();

            var mockPetSet = new Mock<DbSet<Pet>>();

            mockPetSet.As<IQueryable<Pet>>().Setup(m => m.Provider).Returns(pets.Provider);
            mockPetSet.As<IQueryable<Pet>>().Setup(m => m.Expression).Returns(pets.Expression);
            mockPetSet.As<IQueryable<Pet>>().Setup(m => m.ElementType).Returns(pets.ElementType);
            mockPetSet.As<IQueryable<Pet>>().Setup(m => m.GetEnumerator()).Returns(pets.GetEnumerator());

            context.Setup(c => c.Pets).Returns(mockPetSet.Object);
            mapper.Setup(m => m.Map(pets.ToList())).Returns(new List<PetDto>());

            //Act
            var result =  sut.Handle(new GetOwnedPetsQuery(1, now), CancellationToken.None);

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

            mapper.Setup(m => m.Map(pets.ToList())).Returns(new List<PetDto>());

            //Act
            var result = sut.Handle(new GetOwnedPetsQuery(1, now), CancellationToken.None);

            //Assert
            Assert.True(result.Result.ResultType == ResultType.NotFound);
        }
    }
}
