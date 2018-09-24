using System;
using VirtualPet.Logic.Entities;
using VirtualPet.Logic.ValueObjects;
using Xunit;

namespace VirtualPet.Tests
{
    public class PetTests
    {
        [Theory]
        [InlineData(10, -10, 0)]
        [InlineData(10, -1, 40)]
        [InlineData(10, 0, 50)]
        [InlineData(1, -50, 0)]
        public void Pet_decreases_mood_by_modifier_over_time(int moodTimeModifier, int minutes, int expected)
        {
            //Arrange
            var moodTimeMod = moodTimeModifier;
            var startingMood = 50;
            var now = DateTime.Now;
            var lastUpdated = now.AddMinutes(minutes);
            int expectedValue = expected;

            var profile = new PetProfile(new PetProfileVO(moodTimeMod, 0));
            var pet = new Pet(new PetVO("", startingMood, 0, lastUpdated), profile, 0);

            //Act
            var mood = pet.GetMood(now);

            //Assert
            Assert.Equal(expectedValue, mood);
        }

        [Theory]
        [InlineData(10, -10, 100)]
        [InlineData(10, -1, 60)]
        [InlineData(10, 0, 50)]
        [InlineData(1, -50, 100)]
        public void Pet_increases_hunger_by_modifier_over_time(int hungerTimeModifier, int minutes, int expected)
        {
            //Arrange
            var hungerTimeMod = hungerTimeModifier;
            var startingHunger = 50;
            var now = DateTime.Now;
            var lastUpdated = now.AddMinutes(minutes);
            int expectedValue = expected;

            var profile = new PetProfile(new PetProfileVO(0, hungerTimeMod));
            var pet = new Pet(new PetVO("", 0, startingHunger, lastUpdated), profile, 0);

            //Act
            var hunger = pet.GetHunger(now);

            //Assert
            Assert.Equal(expectedValue, hunger);
        }

        [Theory]
        [InlineData(1, 10, -10, 50)]
        [InlineData(1, 10, -100, 10)]
        [InlineData(10, 100, 0, 100)]
        [InlineData(10, 10, 0, 60)]
        public void Pet_stroked_and_mood_increased(int moodTimeModifier, int strokeModifier, int minutes, int expected)
        {
            //Arrange
            var moodMod = moodTimeModifier;
            var strokeMod = strokeModifier;
            var startingMood = 50;
            var now = DateTime.Now;
            var lastUpdated = now.AddMinutes(minutes);
            int expectedValue = expected;
            var profile = new PetProfile(new PetProfileVO(moodMod, 0, strokeMod));
            var pet = new Pet(new PetVO("", startingMood, 0, lastUpdated), profile, 0);

            //Act
            pet.Stroke(now);

            //Assert
            Assert.Equal(expectedValue, pet.State.LastUpdatedMood);
            Assert.Equal(now, pet.State.LastUpdated);
        }

        [Theory]
        [InlineData(1, 10, -10, 50)]
        [InlineData(1, 10, -100, 90)]
        [InlineData(10, 100, 0, 0)]
        [InlineData(10, 10, 0, 40)]
        public void Pet_Fed_and_hunger_decreased(int hungerTimeModifier, int feedModifier, int minutes, int expected)
        {
            //Arrange
            var hungerMod = hungerTimeModifier;
            var feedMod = feedModifier;
            var startingHunger = 50;
            var now = DateTime.Now;
            var lastUpdated = now.AddMinutes(minutes);
            int expectedValue = expected;
            var profile = new PetProfile(new PetProfileVO(0, hungerMod, 0, feedMod));
            var pet = new Pet(new PetVO("", 0, startingHunger, lastUpdated), profile, 0);

            //Act
            pet.Feed(now);

            //Assert
            Assert.Equal(expectedValue, pet.State.LastUpdatedHunger);
            Assert.Equal(now, pet.State.LastUpdated);
        }
    }
}
