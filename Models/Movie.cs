namespace MoviesManagment.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string YearProduction { get; set; }
        public string? Genres { get; set; }//api
        public string? Stars { get; set; }//api

        //public string? Director { get; set; } //api
        //public Movie(Movie movie)
        //{
        //    Id=movie.Id;
        //    Title=movie.Title;
        //    YearProduction=movie.YearProduction;
        //    Genre=movie.Genre;
        //    Director = movie.Director;
        //}
    }
}
