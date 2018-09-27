using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VirtualPet.Api.Controllers;
using VirtualPet.Application.Dtos;
using VirtualPet.Application.HandlerResponse;
using VirtualPet.Application.Queries;
using Xunit;

namespace VirtualPet.WebApi.UnitTests
{
    public class PetControllerTests
    {
        private readonly PetController sut;
        private readonly Mock<IMediator> mockMediator;

        public PetControllerTests()
        {
            mockMediator = new Mock<IMediator>();
            sut = new PetController(mockMediator.Object);
        }

        [Fact]
        public void Get_Owned_Pets_Returns_Pets()
        {
            //Arrange
            var updateDate = DateTime.Now;
            var ownedId = 1;
            var handlerResponse = new HandlerResponse<List<PetDto>>(ResultType.Success, new List<PetDto>());

            mockMediator.Setup(x => x.Send(It.Is<GetOwnedPetsQuery>(y => y.OwnerId == ownedId), CancellationToken.None)).Returns(Task.FromResult(handlerResponse));

            //Arrange
            var httpResult = sut.GetPets(ownedId);

            //Assert
            Assert.IsType<OkObjectResult>(httpResult.Result);
        }
        [Fact]
        public void Get_Owned_Pets_Returns_NotFound()
        {
            //Arrange
            var updateDate = DateTime.Now;
            var ownedId = 1;
            var handlerResponse = new HandlerResponse<List<PetDto>>(ResultType.NotFound, null);

            mockMediator.Setup(x => x.Send(It.Is<GetOwnedPetsQuery>(y => y.OwnerId == ownedId), CancellationToken.None)).Returns(Task.FromResult(handlerResponse));

            //Arrange
            var httpResult = sut.GetPets(ownedId);

            //Assert
            Assert.IsType<NotFoundResult>(httpResult.Result);
        }
    }
}
