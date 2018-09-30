using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VirtualPet.Api.Controllers;
using VirtualPet.Application.Commands;
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
        public void Get()
        {
            //Arrange
            var httpResult = sut.Get();

            //Assert
            Assert.IsType<OkObjectResult>(httpResult);
        }
        [Fact]
        public void Get_Pet_Returns_Pet()
        {
            //Arrange
            var petId = 1;
            var handlerResponse = new HandlerResponse<PetDto>(ResultType.Success, new PetDto());

            mockMediator.Setup(x => x.Send(It.Is<GetPetQuery>(y => y.PetId == petId), CancellationToken.None)).Returns(Task.FromResult(handlerResponse));

            //Arrange
            var httpResult = sut.Get(petId);

            //Assert
            Assert.IsType<OkObjectResult>(httpResult.Result);
        }
        [Fact]
        public void Get_Pet_Returns_NotFound()
        {
            //Arrange
            var petId = 1;
            var handlerResponse = new HandlerResponse<PetDto>(ResultType.NotFound, null);

            mockMediator.Setup(x => x.Send(It.Is<GetPetQuery>(y => y.PetId == petId), CancellationToken.None)).Returns(Task.FromResult(handlerResponse));

            //Arrange
            var httpResult = sut.Get(petId);

            //Assert
            Assert.IsType<NotFoundResult>(httpResult.Result);
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
            var ownedId = 1;
            var handlerResponse = new HandlerResponse<List<PetDto>>(ResultType.NotFound, null);

            mockMediator.Setup(x => x.Send(It.Is<GetOwnedPetsQuery>(y => y.OwnerId == ownedId), CancellationToken.None)).Returns(Task.FromResult(handlerResponse));

            //Arrange
            var httpResult = sut.GetPets(ownedId);

            //Assert
            Assert.IsType<NotFoundResult>(httpResult.Result);
        }
        [Fact]
        public void Stroke_Pet_Returns_NoContent()
        {
            //Arrange
            var petId = 1;
            var handlerResponse = new HandlerResponse<string>(ResultType.Success);

            mockMediator.Setup(x => x.Send(It.Is<StrokePetCommand>(y => y.PetId == petId), CancellationToken.None)).Returns(Task.FromResult(handlerResponse));

            //Arrange
            var httpResult = sut.Stroke(petId);

            //Assert
            Assert.IsType<NoContentResult>(httpResult.Result);
        }
        [Fact]
        public void Stroke_Pet_Returns_NotFound()
        {
            //Arrange
            var petId = 1;
            var handlerResponse = new HandlerResponse<string>(ResultType.NotFound, "");

            mockMediator.Setup(x => x.Send(It.Is<StrokePetCommand>(y => y.PetId == petId), CancellationToken.None)).Returns(Task.FromResult(handlerResponse));

            //Arrange
            var httpResult = sut.Stroke(petId);

            //Assert
            Assert.IsType<NotFoundResult>(httpResult.Result);
        }
        [Fact]
        public void Feed_Pet_Returns_NoContent()
        {
            //Arrange
            var petId = 1;
            var handlerResponse = new HandlerResponse<string>(ResultType.Success);

            mockMediator.Setup(x => x.Send(It.Is<FeedPetCommand>(y => y.PetId == petId), CancellationToken.None)).Returns(Task.FromResult(handlerResponse));

            //Arrange
            var httpResult = sut.Feed(petId);

            //Assert
            Assert.IsType<NoContentResult>(httpResult.Result);
        }
        [Fact]
        public void Feed_Pet_Returns_NotFound()
        {
            //Arrange
            var petId = 1;
            var handlerResponse = new HandlerResponse<string>(ResultType.NotFound, "");

            mockMediator.Setup(x => x.Send(It.Is<FeedPetCommand>(y => y.PetId == petId), CancellationToken.None)).Returns(Task.FromResult(handlerResponse));

            //Arrange
            var httpResult = sut.Feed(petId);

            //Assert
            Assert.IsType<NotFoundResult>(httpResult.Result);
        }
        [Fact]
        public void Post_Pet_Returns_Created_Pet()
        {
            //Arrange
            var petId = 1;
            var handlerResponse = new HandlerResponse<PetDto>(ResultType.Success, new PetDto());

            mockMediator.Setup(x => x.Send(It.IsAny<CreatePetCommand>(), CancellationToken.None)).Returns(Task.FromResult(handlerResponse));

            //Arrange
            var httpResult = sut.Post(new PetDto());

            //Assert
            Assert.IsType<OkObjectResult>(httpResult.Result);
        }
    }
}
