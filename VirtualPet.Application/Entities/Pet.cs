using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using VirtualPet.Application.ValueObjects;

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
        [ForeignKey("PetProfileId")]
        public PetProfile Profile { get; set; }
        public int PetProfileId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("PetTypeId")]
        public PetType TypeOfPet { get; set; }
        public int PetTypeId { get; set; }
        public List<Event> Events { get; set; }

        public void UpDatePet(DateTime currentDate)
        {
            var multiplier = GetStatMultiplier(currentDate, LastUpdated);
            Mood -= Profile.MoodTimeModifier * multiplier;
            Hunger += Profile.HungerTimeModifier * multiplier;
        }

        public void Stroke(DateTime currentTime)
        {
            UpDatePet(currentTime);
            Mood += Profile.StrokeModifier;
            LastUpdated = currentTime;
        }

        public void Feed(DateTime currentTime)
        {
            UpDatePet(currentTime);
            Hunger -= Profile.FeedModifier;
            LastUpdated = currentTime;
        }

        private int GetStatMultiplier(DateTime currentDate, DateTime lastDate)
        {
            return (int)currentDate.Subtract(lastDate).TotalMinutes;
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
