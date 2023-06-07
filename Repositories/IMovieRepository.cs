using MoviesManagment.Models;

namespace MoviesManagment.Repositories
{
    /// <summary>
    /// Interface for accessing movie data from a repository.
    /// </summary>
    public interface IMovieRepository
    {
        /// <summary>
        /// Retrieves a movie by its ID.
        /// </summary>
        /// <param name="id">The ID of the movie.</param>
        /// <returns>The movie with the specified ID.</returns>
        Movie GetMovie(int id);

        /// <summary>
        /// Retrieves a list of movies.
        /// </summary>
        /// <returns>The list of movies.</returns>
        List<Movie> GetMovies();

        /// <summary>
        /// Creates a new movie.
        /// </summary>
        /// <param name="movie">The movie object to create.</param>
        /// <returns>A task representing the asynchronous create operation.</returns>
        Task<Movie> CreateMovie(Movie movie);

        /// <summary>
        /// Updates an existing movie.
        /// </summary>
        /// <param name="movie">The movie object to update.</param>
        /// <returns>A task representing the asynchronous update operation.</returns>
        Task<Movie> UpdateMovie(Movie movie);

        /// <summary>
        /// Deletes a movie by its ID.
        /// </summary>
        /// <param name="id">The ID of the movie to delete.</param>
        /// <returns>A task representing the asynchronous delete operation.</returns>
        Task<bool> DeleteMovie(int id);
    }

}
