using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<User>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {
            
        }
        
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<UserFilm> UserFilms { get; set; }
        public DbSet<FilmActor> FilmActors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserFilm>(x => x.HasKey(p => new { p.UserId, p.FilmId}));
            builder.Entity<UserFilm>()
                .HasOne(u => u.User)
                .WithMany(u => u.UserFilms)
                .HasForeignKey(p => p.UserId);
            builder.Entity<UserFilm>()
                .HasOne(u => u.Film)
                .WithMany(u => u.UserFilms)
                .HasForeignKey(p => p.FilmId);

            builder.Entity<FilmActor>(x => x.HasKey(fa => new { fa.FilmId, fa.ActorId}));
            builder.Entity<FilmActor>()
                .HasOne(f => f.Film)
                .WithMany(f => f.FilmActors)
                .HasForeignKey(f => f.FilmId);
            builder.Entity<FilmActor>()
                .HasOne(f => f.Actor)
                .WithMany(f => f.FilmActors)
                .HasForeignKey(f => f.ActorId);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }

    }
}