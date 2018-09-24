using Moq;
using System;
using VirtualPet.Api.Dtos;
using VirtualPet.Data.Repositories;
using VirtualPet.Domain.Entities;
using Xunit;

namespace VirtualPet.Api.Tests
{
    interface ITesting
    {

    }
    public class PetControllerTests
    {

        public PetControllerTests()
        {
            
        }

        [Fact]
        public void Get_pet_by_id_returns_pet()
        {
            Mock<PetRepository> mockRepository = new Mock<PetRepository>();

            mockRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Pet());
        }
            
          
    }
}
