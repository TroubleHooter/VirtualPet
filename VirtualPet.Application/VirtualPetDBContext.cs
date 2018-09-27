using System;
using Microsoft.EntityFrameworkCore;
using VirtualPet.Application.Entities;
using VirtualPet.Application.ValueObjects;

namespace VirtualPet.Application
{
    public class VirtualPetDbContext : DbContext
    {
        public virtual DbSet<Pet> Pets { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<PetProfile> Profiles { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventType> EventTypes { get; set; }
        public virtual DbSet<PetType> PetTypes { get; set; }

        //public VirtualPetDbContext(DbContextOptions<VirtualPetDbContext> options) : base(options)
        //{

        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = .\\; Database = VirtualPet; Trusted_Connection = True;");
            optionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            CreateDbDefaults(modelBuilder);
            SeedData(modelBuilder);
        }

        private void CreateDbDefaults(ModelBuilder modelBuilder)
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
        }
        private void SeedData(ModelBuilder modelBuilder)
        {
            var createDate = DateTime.Now.AddMinutes(-10);

            var petTypeDog = new PetType { Id = 1, CreateDate = createDate, Name = "Dog" };
            var petTypeCat = new PetType { Id = 2, CreateDate = createDate, Name = "Cat" };

            var eventTypeBorn = new EventType
            {
                Id = 1,
                CreateDate = createDate,
                Name = "Born",
                Description = "The pet was born"
            };
            var eventTypeStroked = new EventType
            {
                Id = 2,
                CreateDate = createDate,
                Name = "Stroked",
                Description = "The pet was stroked"
            };
            ;
            var eventTypeFeed = new EventType
            {
                Id = 3,
                CreateDate = createDate,
                Name = "Fed",
                Description = "The pet was fed"
            };

            var petProfile = new PetProfile
            {
                Id = 1,
                CreateDate = createDate,
                MoodTimeModifier = 1,
                HungerTimeModifier = 1,
                FeedModifier = 10,
                StrokeModifier = 10
                
            };

            var bornEvent = new Event {Id = 1, CreateDate = createDate, EventTypeId = 1, PetId = 1};

            var pet = new Pet
            {
                Id = 1,
                Name = "Fido",
                UserId = 1,
                Hunger = 50,
                Mood = 50,
                CreateDate = createDate,
                LastUpdated = createDate,
                PetProfileId = 1,
                PetTypeId = 1
            };

            var user = new User
            {
                Id = 1,
                CreateDate = createDate
            };

            modelBuilder.Entity<Event>().HasData(bornEvent);

            modelBuilder.Entity<PetProfile>().HasData(petProfile);

            modelBuilder.Entity<EventType>().HasData(eventTypeBorn);
            modelBuilder.Entity<EventType>().HasData(eventTypeStroked);
            modelBuilder.Entity<EventType>().HasData(eventTypeFeed);

            modelBuilder.Entity<PetType>().HasData(petTypeDog);
            modelBuilder.Entity<PetType>().HasData(petTypeCat);

            modelBuilder.Entity<User>().HasData(user);
            modelBuilder.Entity<Pet>().HasData(pet);

          //  Database.EnsureCreated();

        }
    }
}
 