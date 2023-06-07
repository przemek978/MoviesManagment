using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesManagment.Models;
using MoviesManagment.Services;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace MoviesManagement.Controllers
{
    /// <summary>
    /// Controller class for managing movies.
    /// </summary>
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MoviesController"/> class.
        /// </summary>
        /// <param name="movieService">The movie service.</param>
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// Action method for the Index view.
        /// </summary>
        /// <returns>The view displaying a list of movies.</returns>
        public async Task<IActionResult> Index()
        {
            var movies = _movieService.GetMovies();
            return View(movies);
        }

        /// <summary>
        /// Action method for the Details view.
        /// </summary>
        /// <param name="id">The ID of the movie.</param>
        /// <returns>The view displaying details of a movie from API.</returns>
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _movieService.GetMovies() == null)
            {
                return NotFound();
            }

            var movie = _movieService.GetMovie(id);
            var newMovie = await _movieService.SearchMovies(movie);

            if (movie == null || newMovie == null)
            {
                return NotFound();
            }

            return View(newMovie);
        }

        /// <summary>
        /// Action method for the Edit operation.
        /// </summary>
        /// <param name="request">Object to connect to telerik.</param>
        /// <param name="movie">The movie object to be edited.</param>
        /// <returns>The JSON result for the Edit operation.</returns>
        public async Task<IActionResult> Edit([DataSourceRequest] DataSourceRequest request, Movie movie)
        {
            if (ModelState.IsValid)
            {
                await _movieService.UpdateMovie(movie);
                return RedirectToAction("Index");
            }

            return Json(new[] { movie }.ToDataSourceResult(request, ModelState));
        }

        /// <summary>
        /// Action method for the Create operation.
        /// </summary>
        /// <param name="request">Object to connect to telerik.</param>
        /// <param name="movie">The movie object to be created.</param>
        /// <returns>The JSON result for the Create operation.</returns>
        public async Task<IActionResult> Create([DataSourceRequest] DataSourceRequest request, Movie movie)
        {
            if (ModelState.IsValid)
            {
                await _movieService.CreateMovie(movie);
            }

            return Json(new[] { movie }.ToDataSourceResult(request, ModelState));
        }

        /// <summary>
        /// Action method for the Delete operation.
        /// </summary>
        /// <param name="request">Object to connect to telerik.</param>
        /// <param name="movie">The movie object to be deleted.</param>
        /// <returns>The JSON result for the Delete operation.</returns>
        public async Task<IActionResult> Delete([DataSourceRequest] DataSourceRequest request, Movie movie)
        {
            await _movieService.DeleteMovie(movie.Id);

            return Json(new[] { movie }.ToDataSourceResult(request, ModelState));
        }
    }
}
