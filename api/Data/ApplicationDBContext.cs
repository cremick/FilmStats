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
        
        public DbSet<Person> People { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<UserFilm> UserFilms { get; set; }
        public DbSet<FilmActor> FilmActors { get; set; }
        public DbSet<FilmGenre> FilmGenres { get; set; }
        public DbSet<FilmTheme> FilmThemes { get; set; }
        public DbSet<FilmDirector> FilmDirectors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserFilm>(x => x.HasKey(uf => new { uf.UserId, uf.FilmId}));
            builder.Entity<UserFilm>()
                .HasOne(uf => uf.User)
                .WithMany(u => u.UserFilms)
                .HasForeignKey(uf => uf.UserId);
            builder.Entity<UserFilm>()
                .HasOne(uf => uf.Film)
                .WithMany(f => f.UserFilms)
                .HasForeignKey(uf => uf.FilmId);

            builder.Entity<FilmActor>(x => x.HasKey(fa => new { fa.FilmId, fa.ActorId}));
            builder.Entity<FilmActor>()
                .HasOne(fa => fa.Film)
                .WithMany(f => f.FilmActors)
                .HasForeignKey(fa => fa.FilmId);
            builder.Entity<FilmActor>()
                .HasOne(fa => fa.Actor)
                .WithMany(a => a.FilmActors)
                .HasForeignKey(fa => fa.ActorId);

            builder.Entity<FilmGenre>(x => x.HasKey(fg => new { fg.FilmId, fg.GenreId}));
            builder.Entity<FilmGenre>()
                .HasOne(fg => fg.Film)
                .WithMany(f => f.FilmGenres)
                .HasForeignKey(fg => fg.FilmId);
            builder.Entity<FilmGenre>()
                .HasOne(fg => fg.Genre)
                .WithMany(g => g.FilmGenres)
                .HasForeignKey(fg => fg.GenreId);

            builder.Entity<FilmTheme>(x => x.HasKey(ft => new { ft.FilmId, ft.ThemeId}));
            builder.Entity<FilmTheme>()
                .HasOne(ft => ft.Film)
                .WithMany(f => f.FilmThemes)
                .HasForeignKey(ft => ft.FilmId);
            builder.Entity<FilmTheme>()
                .HasOne(ft => ft.Theme)
                .WithMany(t => t.FilmThemes)
                .HasForeignKey(ft => ft.ThemeId);

            builder.Entity<FilmDirector>(x => x.HasKey(fd => new { fd.FilmId, fd.DirectorId}));
            builder.Entity<FilmDirector>()
                .HasOne(fd => fd.Film)
                .WithMany(f => f.FilmDirectors)
                .HasForeignKey(fd => fd.FilmId);
            builder.Entity<FilmDirector>()
                .HasOne(fd => fd.Director)
                .WithMany(d => d.FilmDirectors)
                .HasForeignKey(fd => fd.DirectorId);

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