using System;
using VirtualPet.Application.Entities;
using Xunit;

namespace VirtualPet.Domain.Tests
{
    public class PetTests
    {
        [Theory]
        [InlineData(1, 1, 10, -10, 50, 60)]
        [InlineData(1, 1, 10, -100, 10, 100)]
        [InlineData(10, 1, 100, 0, 100, 50)]
        [InlineData(10, 1, 10, 0, 60, 50)]
        [InlineData(10, 10, 10, -20, 10, 100)]
        public void Pet_stroked_and_mood_increased(int moodTimeModifier, int hungerTimeModifier, int strokeModifier, int minutes, int expectedMood, int expectedHunger)
        {
            //Arrange
            var moodMod = moodTimeModifier;
            var hungerMod = hungerTimeModifier;
            var strokeMod = strokeModifier;
            var now = DateTime.Now;
            var lastUpdated = now.AddMinutes(minutes);
            int exHunger = expectedHunger;
            int exMood = expectedMood;
            var profile = new PetProfile{ MoodTimeModifier = moodMod, HungerTimeModifier = hungerMod, StrokeModifier = strokeMod };
            var pet = new Pet {LastUpdated = lastUpdated, Profile = profile};

            //Act
            pet.Stroke(now);

            //Assert
            Assert.Equal(exMood, pet.Mood);
            Assert.Equal(exHunger, pet.Hunger);
            Assert.Equal(now, pet.LastUpdated);
        }

        [Theory]
        [InlineData(1, 1, 10, -10, 50, 40)]
        [InlineData(1, 1, 10, -100, 90, 0)]
        [InlineData(10, 1, 100, 0, 0, 50)]
        [InlineData(10, 1, 10, 0, 40, 50)]
        [InlineData(10, 10, 10, -20, 90, 0)]
        public void Pet_Fed_and_hunger_decreased(int hungerTimeModifier, int moodTimeModifier, int feedModifier, int minutes, int expectedHunger, int expectedMood)
        {
            //Arrange
            var hungerMod = hungerTimeModifier;
            var moodMod = moodTimeModifier;
            var feedMod = feedModifier;
            var now = DateTime.Now;
            var lastUpdated = now.AddMinutes(minutes);
            int exHunger = expectedHunger;
            var exMood = expectedMood;
            var profile = new PetProfile { HungerTimeModifier = hungerMod, MoodTimeModifier = moodMod, FeedModifier = feedMod };
            var pet = new Pet {LastUpdated = lastUpdated, Profile = profile};

            //Act
            pet.Feed(now);

            //Assert
            Assert.Equal(exHunger, pet.Hunger);
            Assert.Equal(exMood, pet.Mood);
            Assert.Equal(now, pet.LastUpdated);
        }
    }
}
