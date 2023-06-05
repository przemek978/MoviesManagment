using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MoviesManagment.Models
{
    public class Movie
    {
        [Key]
        [ScaffoldColumn(false)]
        [DisplayName("ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tytuł jest wymagany")]
        [DisplayName("Tytuł")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Rok produkcji jest wymagany")]
        [DisplayName("Rok produkcji")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Rok produkcji musi byc liczba w formacie YYYY")]
        public string ReleaseYear { get; set; }

        [DisplayName("Reżyser")]
        public string? Director { get; set; }

        [DisplayName("Gatunki")]
        [RegularExpression(@"^([A-Za-z]+[-\s]*,\s*)*[A-Za-z]+[-\s]*$", ErrorMessage = "Nieprawidłowy format gatunków.")]
        public string? Genres { get; set; }//api

        [DisplayName("Gwiazdy")]
        [RegularExpression(@"^([A-Za-z]+[-\s]*,\s*)*[A-Za-z]+[-\s]*$", ErrorMessage = "Nieprawidłowy format gwiazd.")]
        public string? Stars { get; set; }//api

        [DisplayName("Ocena")]
        public string? ImdbRating { get; set; }

        [DisplayName("Fabuła")]
        public string? Plot { get; set; }

        //public string? Director { get; set; } //api
        //public Movie(Movie movie)
        //{
        //    Id=movie.Id;
        //    Title=movie.Title;
        //    ReleaseYear=movie.ReleaseYear;
        //    Genre=movie.Genre;
        //    Director = movie.Director;
        //}
    }
}
