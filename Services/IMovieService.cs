using MoviesManagment.Models;

namespace MoviesManagment.Services
{
    public interface IMovieService
    {
        Task<Movie> SearchMovies(Movie movie);
        Movie GetMovie(int id);
        List<Movie> GetMovies();
        Task<Movie> CreateMovie(Movie movie);
        Task<Movie> UpdateMovie(Movie movie);
        Task<bool> DeleteMovie(int id);
    }

}
