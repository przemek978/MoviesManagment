using Microsoft.EntityFrameworkCore;
using MoviesManagment.Data;
using MoviesManagment.Models;
using System;

namespace MoviesManagment.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieContext _context;

        public MovieRepository(MovieContext context)
        {
            _context = context;
        }

        public Movie GetMovie(int id)
        {
            try
            {
                return _context.Movies.FirstOrDefault(m => m.Id == id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Movie> GetMovies()
        {
            try
            {
                return _context.Movies.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Movie> CreateMovie(Movie movie)
        {
            try
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return movie;
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public async Task<Movie> UpdateMovie(Movie movie)
        {
            try
            {
                var originalMovie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == movie.Id);

                if (originalMovie != null)
                {
                    originalMovie.Title = movie.Title;
                    originalMovie.ReleaseYear = movie.ReleaseYear;
                    originalMovie.Director = movie.Director;
                    originalMovie.Genres = movie.Genres;
                    originalMovie.Stars = movie.Stars;
                    originalMovie.ImdbRating = movie.ImdbRating;
                    await _context.SaveChangesAsync();
                }

                return originalMovie;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeleteMovie(int id)
        {
            try
            {
                var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == 10);

                if (movie != null)
                {
                    _context.Movies.Remove(movie);
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
