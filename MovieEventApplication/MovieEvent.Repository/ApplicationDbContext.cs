using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieEvent.Domain.DomainModels;
using MovieEvent.Domain.IdentityModels;

namespace MovieEvent.Repository
{
    public class ApplicationDbContext : IdentityDbContext<MovieUser>
    {

        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<MovieRating> MovieRatings { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Screening> Screenings { get; set; }
        public virtual DbSet<UserMovieRating> UserMovieRatings { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
