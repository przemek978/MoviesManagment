using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MoviesManagment.Models
{
    public class Movie
    {
        [Key]
        [DisplayName("ID")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tytuł jest wymagany")]
        [DisplayName("Tytuł")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Rok produkcji jest wymagany")]
        [DisplayName("Rok produkcji")]
        public string YearProduction { get; set; }
        [DisplayName("Reżyser")]
        public string? Director { get; set; }
        [DisplayName("Gatunki")]
        public string? Genres { get; set; }//api
        [DisplayName("Gwiazdy")]
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
