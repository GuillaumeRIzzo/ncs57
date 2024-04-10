using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Watchlist.Models;

namespace Watchlist.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Film> Film { get; set; }
        public DbSet<FilmUtilisateur> FilmUtilisateur { get; set; }
        public DbSet<ModeleVueFilm> ModeleVueFilm { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<FilmUtilisateur>()
                .HasKey(t => new { t.IdUtilisateur, t.IdFilm });
            modelBuilder.Entity<ModeleVueFilm>()
                .HasKey(t => new { t.IdFilm });
        }
    }
}
