using System.Text;
using Microsoft.EntityFrameworkCore;
using MoviesManagment.Data;
using MoviesManagment.Models;
using MoviesManagment.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MoviesManagment.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly HttpClient httpClient;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://imdb-api.com");
        }

        public async Task<Movie> SearchMovies(Movie movie)
        {
            try
            {
                StringBuilder endpoint = new StringBuilder($"/API/AdvancedSearch/k_e3j097kj?title=" + movie.Title + "&release_date=" + movie.ReleaseYear + "-01-01," + movie.ReleaseYear + "-12-31");

                if (movie.ImdbRating != null)
                {
                    endpoint.Append("&imDbRating=" + movie.ImdbRating);
                }
                if (movie.Genres != null)
                {
                    endpoint.Append("&genres=" + movie.Genres);
                }
                if (movie.Stars != null)
                {
                    endpoint.Append("&stars=" + movie.Stars);
                }

                HttpResponseMessage response = await httpClient.GetAsync(endpoint.ToString());

                response.EnsureSuccessStatusCode();

                string result = await response.Content.ReadAsStringAsync();

                var searchResult = JObject.Parse(result);
                JArray results = (JArray)searchResult["results"];

                var search = results.OrderByDescending(r => r["title"].ToString().Equals(movie.Title)).FirstOrDefault();

                if (search != null)
                {
                    var stars = search["stars"].ToString();
                    movie.Director = search["stars"].ToString().Split(',')[0];
                    movie.Stars = stars.Substring(stars.IndexOf(", ") + 2);
                    movie.Genres = search["genres"].ToString();
                    movie.ImdbRating = search["imDbRating"].ToString();
                }

                return movie;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Wystąpił błąd podczas wysyłania żądania HTTP: " + ex.Message);
            }
            catch (JsonException ex)
            {
                throw new Exception("Wystąpił błąd podczas parsowania odpowiedzi JSON: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Wystąpił nieoczekiwany błąd: " + ex.Message);
            }
        }

        public Movie GetMovie(int id)
        {
            return _movieRepository.GetMovie(id);
        }

        public List<Movie> GetMovies()
        {
            return _movieRepository.GetMovies();
        }

        public Task<Movie> CreateMovie(Movie movie)
        {
            return _movieRepository.CreateMovie(movie);
        }

        public Task<Movie> UpdateMovie(Movie movie)
        {
            return _movieRepository.UpdateMovie(movie);
        }

        public Task<bool> DeleteMovie(int id)
        {
            return _movieRepository.DeleteMovie(id);
        }
    }

}
