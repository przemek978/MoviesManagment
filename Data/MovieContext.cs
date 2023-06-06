using Microsoft.EntityFrameworkCore;
using MoviesManagment.Models;

namespace MoviesManagment.Data
{
    public class MovieContext : DbContext
    {

        public MovieContext(DbContextOptions<MovieContext> options)
            : base(options)
        {

        }
        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

    }
}
