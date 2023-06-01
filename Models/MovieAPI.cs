using System.Text;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MoviesManagment.Models
{
    public static class MovieAPI
    {
        private static HttpClient httpClient;

        static MovieAPI()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://imdb-api.com");
        }

        public static async Task<Movie> SearchMovies(Movie movie)
        {
           StringBuilder endpoint =new StringBuilder($"/API/AdvancedSearch/k_ou27i01z?title=" + movie.Title + "&release_date=" + movie.YearProduction + "-01-01," + movie.YearProduction + "-12-31");

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

            var search= results.FirstOrDefault(r => r["title"].ToString().Equals(movie.Title, StringComparison.OrdinalIgnoreCase));
            var stars = search["stars"].ToString();
            movie.Director = search["stars"].ToString().Split(',')[0];
            movie.Stars = stars.Substring(stars.IndexOf(", ") + 2); ;
            movie.Genres = search["genres"].ToString();

            return  movie;
        }
    }
}
