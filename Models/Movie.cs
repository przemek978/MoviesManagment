using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MoviesManagment.Models
{
    /// <summary>
    /// Represents information about a movie.
    /// </summary>
    public class Movie
    {
        // Movie ID.
        [Key]
        [ScaffoldColumn(false)]
        [DisplayName("ID")]
        public int Id { get; set; }

        // The title of the movie on the basis of which data in the API are searched.
        [DisplayName("Tytuł")]
        [Required(ErrorMessage = "Tytuł jest wymagany")]
        public string Title { get; set; }

        // The year of manufacture on the basis of which data is searched in the API.
        [Required(ErrorMessage = "Rok produkcji jest wymagany")]
        [DisplayName("Rok produkcji")]
        [RegularExpression(@"^(19[0-9]{2}|20[0-2][0-9]|2030)$", ErrorMessage = "Rok produkcji musi byc liczba w formacie YYYY od 1900 do 2030")]
        public string ReleaseYear { get; set; }

        // Species are not required but are given after a comma will be included in the search.
        [DisplayName("Gatunki")]
        [RegularExpression(@"^([^0-9]+,)*[^0-9]+$", ErrorMessage = "Nieprawidłowy format gatunków. Wymieniaj gatunki po przecinku bez cyfr")]
        public string? Genres { get; set; }

        //Stars are not required but when given after a decimal point will be included in the search.
        [DisplayName("Gwiazdy")]
        [RegularExpression(@"^([^0-9]+,)*[^0-9]+$", ErrorMessage = "Nieprawidłowy format gwiazd. Wymieniaj gwiazdy po przecinku bez cyfr")]
        public string? Stars { get; set; }//api

        //Rating that is not required but when given in the range of 1.0 - 10.0 will be included in the search.
        [DisplayName("Ocena")]
        [RegularExpression(@"^(10\.0|[1-9]\.[0-9])$", ErrorMessage = "Nieprawidłowy format oceny. Podaj liczbe z zakresu 1.0 - 10.0")]
        public string? ImdbRating { get; set; }

        //Director is not required and is not included in the search.
        [DisplayName("Reżyser")]
        [RegularExpression(@"^[^0-9,]+$", ErrorMessage ="Nieprawidłowy format reżysera. Podaj jednego reżysera nie używając cyfr")]
        public string? Director { get; set; }
    }
}
