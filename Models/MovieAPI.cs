using System.Text;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MoviesManagment.Models
{
    public static class MovieAPI
    {
        private static readonly HttpClient httpClient;

        public MovieAPI()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://imdb-api.com");
        }

        public static async Task<Movie> SearchMovies(Movie movie)
        {
            string endpoint = $"/API/AdvancedSearch/k_ou27i01z?title=" + movie.Title + "&release_date=" + movie.YearProduction + "-01-01," + movie.YearProduction + "-12-31";

            HttpResponseMessage response = await httpClient.GetAsync(endpoint);

            response.EnsureSuccessStatusCode();

            string result = await response.Content.ReadAsStringAsync();

            var searchResult = JObject.Parse(result); ;
            JArray results = (JArray)searchResult["results"]; 

            var search= results.FirstOrDefault(r => r["title"].ToString().Equals(movie.Title, StringComparison.OrdinalIgnoreCase));
            //var titles = results.Select(result => (string)result["title"]).ToArray();
            //var a = new { Genres = search["genres"].ToString(), Stars= search["stars"].ToString()  };

            movie.Genres = search["genres"].ToString();
            movie.Stars = search["stars"].ToString();

            return  movie;
        }
    }
}
