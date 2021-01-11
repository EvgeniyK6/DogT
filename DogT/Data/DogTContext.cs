using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogT.Models;

namespace DogT.Data
{
    public class DogTContext : DbContext
    {
        public DogTContext(DbContextOptions<DogTContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<DogHandler> DogHandlers { get; set; }
        public DbSet<Dog> Dogs { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<TrainingComment> TrainingComments { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        //{
        //    dbContextOptionsBuilder.EnableSensitiveDataLogging();
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Role adminRole = new Role { Id = 1, Title = "Адміністратор" };
            Role dogHandlerRole = new Role { Id = 2, Title = "Кінолог" };

            string adminEmail = "admin@mail";
            string adminPassword = "admin";

            User admin = new User { Id = 1, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id};


            modelBuilder.Entity<Role>()
                .HasData(new Role[] { adminRole, dogHandlerRole });
            
            modelBuilder.Entity<User>()
                .HasData(new User[] { admin });

            modelBuilder.Entity<Role>()
                .HasMany(u => u.Users)
                .WithOne(r => r.Role)
                .HasForeignKey(rk => rk.RoleId);

            modelBuilder.Entity<Specialization>()
                .HasMany(t => t.Trainings)
                .WithOne(s => s.Specialization)
                .HasForeignKey(sk => sk.SpecializationId)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<TrainingComment>()
                .HasOne(t => t.Training)
                .WithMany(s => s.Comments)
                .HasForeignKey(sk => sk.TrainingId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Training>()
                .HasOne(d => d.Dog)
                .WithMany(t => t.Trainings)
                .HasForeignKey(fk => fk.DogId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
