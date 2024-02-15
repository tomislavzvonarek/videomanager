using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VideoManager.Models;

namespace VideoManager.Data
{
	public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
			
		}

        public DbSet<Tag> Tags { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //for identity
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tag>().HasData(
                new Tag { Id = 1, Name = "Animals"},
                new Tag { Id = 2, Name = "Test"},
                new Tag { Id = 3, Name = "Cars"}
                );

            modelBuilder.Entity<Video>().HasData(
                new Video { Id = 1, Name = "Funny Cats", Description = "Cats jumping wild", StreamingUrl = "https://www.cats.com", TotalTime = 12, TagId = 1, GenreId = 2, ImageUrl = ""},
                new Video { Id = 2, Name = "Sports Cars", Description = "Cars drifting wild", StreamingUrl = "https://www.cars.com", TotalTime = 8, TagId = 3, GenreId = 1, ImageUrl = ""},
                new Video { Id = 3, Name = "Funny People", Description = "People falling", StreamingUrl = "https://www.people.com", TotalTime = 21, TagId = 2, GenreId = 2, ImageUrl = ""}
            );

            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Action", Description = "Action videos with guns"},
                new Genre { Id = 2, Name = "Documentary", Description = "Documentary videos"},
                new Genre { Id = 3, Name = "Sport", Description = "Sport videos"}
            );

            modelBuilder.Entity<Country>().HasData(
                new Country { Id = 1, Code = "HR", Name = "Croatia"},
                new Country { Id = 2, Code = "GE", Name = "Germany"},
                new Country { Id = 3, Code = "EN", Name = "England"}
            );

        }


    }
}

