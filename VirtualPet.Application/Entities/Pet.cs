using System;
using System.Collections.Generic;

namespace VirtualPet.Application.Entities
{
    public class Pet : Entity
    {
        private int _mood;
        private int _hunger;
        public int Mood
        {
            get => _mood;
            set => _mood = ConstrainStat(value);
        }
        public int Hunger
        {
            get => _hunger;
            set => _hunger = ConstrainStat(value);
        }
        public string Name { get; set; }
        public DateTime LastUpdated { get; set; }
        public PetProfile Profile { get; set; }
        public int UserId { get; set; }
        public PetType TypeOfPet { get; set; }
        public List<Event> Events { get; set; }

        private void UpdateMood(DateTime currentDate)
        {
            var multiplier = GetStatMultiplier(currentDate, LastUpdated);
            Mood -= Profile.MoodTimeModifier * multiplier;
        }

        public void UpdateHunger(DateTime currentDate)
        {
            var multiplier = GetStatMultiplier(currentDate, LastUpdated);
            Hunger += Profile.HungerTimeModifier * multiplier;
        }

        public void Stroke(DateTime currentTime)
        {
            UpdateMood(currentTime);
            UpdateHunger(currentTime);
            Mood += Profile.StrokeModifier;
            LastUpdated = currentTime;
        }

        public void Feed(DateTime currentTime)
        {
            UpdateMood(currentTime);
            UpdateHunger(currentTime);
            Hunger -= Profile.FeedModifier;
            LastUpdated = currentTime;
        }

        private int GetStatMultiplier(DateTime currentDate, DateTime LastDate)
        {
            return (int)currentDate.Subtract(LastDate).TotalMinutes;
        }
        private int ConstrainStat(int matrix)
        {
            if (matrix > 100)
                return 100;
            if (matrix < 0)
                return 0;

            return matrix;
        }
    }
}
