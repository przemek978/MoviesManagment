using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesManagment.Data;
using MoviesManagment.Models;

namespace MoviesManagment.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieContext _context;

        public MoviesController(MovieContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            if (_context.Movies != null)
            {
                var data = await _context.Movies.ToListAsync();
                
                foreach (var item in data)
                {
                    await MovieAPI.SearchMovies(item);
                }
                return View(data);

            }
            return Problem("Entity set 'MovieContext.Movies'  is null.");
        }


        public async Task<IActionResult>  Edit([DataSourceRequest] DataSourceRequest request, Movie movie)
        {
            try
            {
                var originalMovie = _context.Movies.FirstOrDefault(m => m.Id == movie.Id);

                if (originalMovie != null)
                {
                    await MovieAPI.SearchMovies(originalMovie);
                    originalMovie.Title = movie.Title;
                    originalMovie.ReleaseYear = movie.ReleaseYear;
                    originalMovie.Genres = movie.Genres;
                    originalMovie.Stars = movie.Stars;
                    await _context.SaveChangesAsync();
                }
                return Json(new[] { originalMovie}.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                // Obs³u¿ b³¹d i zwróæ odpowiedŸ z b³êdem jako JSON
                ModelState.AddModelError("", "Wyst¹pi³ b³¹d podczas aktualizacji filmu.");
                return Json(new[] { movie }.ToDataSourceResult(request, ModelState));
            }
        }

        public async Task<IActionResult> Create([DataSourceRequest] DataSourceRequest request, Movie movie)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    await MovieAPI.SearchMovies(movie);
                    _context.Add(movie);
                    _context.SaveChangesAsync();
                }
                return Json(new[] { movie }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                // Obs³u¿ b³¹d i zwróæ odpowiedŸ z b³êdem jako JSON
                ModelState.AddModelError("", "Wyst¹pi³ b³¹d podczas aktualizacji filmu.");
                return Json(new[] { movie }.ToDataSourceResult(request, ModelState));
            }

            return Json(new[] { movie }.ToDataSourceResult(request, ModelState));
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete([DataSourceRequest] DataSourceRequest request, Movie movie)
        {
            try
            {
                // Usuñ film z bazy danych
                _context.Movies.Remove(movie);
                _context.SaveChanges();

                // Zwróæ potwierdzenie usuniêcia jako JSON
                return Json(new[] { movie }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                // Obs³u¿ b³¹d i zwróæ odpowiedŸ z b³êdem jako JSON
                ModelState.AddModelError("", "Wyst¹pi³ b³¹d podczas usuwania filmu.");
                return Json(new[] { movie }.ToDataSourceResult(request, ModelState));
            }

        }

       

        private bool MovieExists(int id)
        {
            return (_context.Movies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
