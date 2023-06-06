using MoviesManagment.Models;

namespace MoviesManagment.Repositories
{
    public interface IMovieRepository
    {
        Movie GetMovie(int id);
        List<Movie> GetMovies();
        Task<Movie> CreateMovie(Movie movie);
        Task<Movie> UpdateMovie(Movie movie);
        Task<bool> DeleteMovie(int id);
    }
}
