using System.Text;
using Kendo.Mvc.UI;
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
            try
            {
                StringBuilder endpoint = new StringBuilder($"/API/AdvancedSearch/k_87jl1kga?title=" + movie.Title + "&release_date=" + movie.ReleaseYear + "-01-01," + movie.ReleaseYear + "-12-31");

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

                //var search = results.FirstOrDefault(r => r["title"].ToString().Equals(movie.Title, StringComparison.OrdinalIgnoreCase));
                
                var search = results.OrderByDescending( r => r["title"].ToString().Equals(movie.Title)).FirstOrDefault();

                if (search != null)
                {
                    var stars = search["stars"].ToString();
                    movie.Director = search["stars"].ToString().Split(',')[0];
                    movie.Stars = stars.Substring(stars.IndexOf(", ") + 2); 
                    movie.Genres = search["genres"].ToString();
                    movie.Plot= search["plot"].ToString();
                    movie.ImdbRating = search["imDbRating"].ToString();
                    //movie.ImdbRating = Convert.ToDecimal(search["imDbRating"].ToString().Replace('.', ','));
                }

                return movie;
            }
            catch (Exception ex)
            {

            }

            return null;

        }
    }
}
