using VirtualPet.Application.Entities;
using Xunit;

namespace VirtualPet.Application.UnitTests.Entities
{
    public class FakeEntity : Entity
    {
        public string StringValue { get; set; }
        public int IntValue { get; set; }
    }

    public class EntityTests
    {

        [Fact]
        public void Entity_Does_Not_Match()
        {
            //Arrange
            var a = new FakeEntity {Id = 1, IntValue = 1, StringValue = "value"};
            var b = new FakeEntity {Id = 2, IntValue = 1, StringValue = "value"};

            //Act
            var result = a.Equals(b);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Entity_Does_Match()
        {
            //Arrange
            var a = new FakeEntity {Id = 1, IntValue = 1, StringValue = "value"};
            var b = new FakeEntity {Id = 1, IntValue = 12, StringValue = "value1"};

            //Act
            var result = a.Equals(b);

            //Assert
            Assert.True(result);
        }
    }
}
