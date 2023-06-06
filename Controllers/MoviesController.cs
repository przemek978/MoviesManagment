using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesManagment.Models;
using MoviesManagment.Services;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace MoviesManagment.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task<IActionResult> Index()
        {
            var movies = _movieService.GetMovies();
            return View(movies);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _movieService.GetMovies() == null)
            {
                return NotFound();
            }

            var movie = _movieService.GetMovie(id);
            var newMovie = await _movieService.SearchMovies(movie);
            
            if (movie == null || newMovie==null)
            {
                return NotFound();
            }

            return View(newMovie);
        }

        public async Task<IActionResult> Edit([DataSourceRequest] DataSourceRequest request, Movie movie)
        {
            if (ModelState.IsValid)
            {
                await _movieService.UpdateMovie(movie);
                return RedirectToAction("Index");
            }

            return Json(new[] { movie }.ToDataSourceResult(request, ModelState));
        }

        public async Task<IActionResult> Create([DataSourceRequest] DataSourceRequest request, Movie movie)
        {
            if (ModelState.IsValid)
            {
                await _movieService.CreateMovie(movie);
            }

            return Json(new[] { movie }.ToDataSourceResult(request, ModelState));
        }

        public async Task<IActionResult> Delete([DataSourceRequest] DataSourceRequest request, Movie movie)
        {
            await _movieService.DeleteMovie(movie.Id);

            return Json(new[] { movie }.ToDataSourceResult(request, ModelState));
        }
    }
}