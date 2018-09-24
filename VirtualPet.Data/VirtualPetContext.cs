using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VirtualPet.Domain.Entities;

namespace VirtualPet.Data
{
    public class VirtualPetContext : DbContext
    {
        public DbSet<Pet> Pets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PetProfile> Profiles { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<PetType> PetTypes { get; set; }

        //public VirtualPetContext(DbContextOptions options) : base(options)
        //{
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = .\\; Database = VirtualPet; Trusted_Connection = True;");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(b => b.CreateDate)
                .HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Pet>()
                .Property(b => b.CreateDate)
                .HasDefaultValueSql("getdate()");
            modelBuilder.Entity<PetProfile>()
                .Property(b => b.CreateDate)
                .HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Event>()
                .Property(b => b.CreateDate)
                .HasDefaultValueSql("getdate()");
            modelBuilder.Entity<EventType>()
                .Property(b => b.CreateDate)
                .HasDefaultValueSql("getdate()");
            modelBuilder.Entity<PetType>()
                .Property(b => b.CreateDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Pet>()
                .Property(b => b.Hunger)
                .HasDefaultValue(50);
            modelBuilder.Entity<Pet>()
                .Property(b => b.Mood)
                .HasDefaultValue(50);

            var createDate = DateTime.Now;

            var petTypeDog = new PetType { Id = 1, CreateDate = createDate, Name = "Dog" };
            var petTypeCat = new PetType { Id = 2, CreateDate = createDate, Name = "Cat"};

            var petProfile = new PetProfile { Id = 1, CreateDate = createDate, MoodTimeModifier = 1, HungerTimeModifier = 1, FeedModifier = 10, StrokeModifier = 10};

            var pet = new Pet{ Id = 1, CreateDate = createDate, Profile = petProfile};
            var user = new User{Id = 1};
            user.Pets = new List<Pet>{ pet };




            modelBuilder.Entity<User>().HasData(user);
            modelBuilder.Entity<User>().HasData(new User());

        }
    }
}
 