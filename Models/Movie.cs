using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MoviesManagment.Models
{
    /// <summary>
    /// Represents information about a movie.
    /// </summary>
    public class Movie
    {
        // Movie ID
        [Key]
        [ScaffoldColumn(false)]
        [DisplayName("ID")]
        public int Id { get; set; }

        //The title of the movie on the basis of which data in the API are searched
        [Required(ErrorMessage = "Tytuł jest wymagany")]
        [DisplayName("Tytuł")]
        public string Title { get; set; }

        //The year of manufacture on the basis of which data is searched in the API
        [Required(ErrorMessage = "Rok produkcji jest wymagany")]
        [DisplayName("Rok produkcji")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Rok produkcji musi byc liczba w formacie YYYY")]
        public string ReleaseYear { get; set; }

        //Species are not required but are given after a comma will be included in the search
        [DisplayName("Gatunki")]
        //Możliwe podanie gatunków po przecinku które zawierja myslnik np. Sci-fi, cyfry ani spacje niedozowolone
        [RegularExpression(@"^[A-Za-z]+(?:-[A-Za-z]+)?(?:,\s*[A-Za-z]+(?:-[A-Za-z]+)?)*$", ErrorMessage = "Nieprawidłowy format gatunków.")]
        public string? Genres { get; set; }

        //Stars are not required but when given after a decimal point will be included in the search
        [DisplayName("Gwiazdy")]
        //Możliwe podawanie imion i nazwisk w roznych formatach do dwoch imion i nazwisk z myslnikami lub spacjami, cyfry niedozowolne
        [RegularExpression(@"^(?:[A-Z][a-z]+\s+(?:[A-Z][a-z]+\s*)?(?:[A-Z][a-z]+(?:-[A-Z][a-z]+)?)?(?:,\s*)?)+(?:,\s*(?:[A-Z][a-z]+\s+(?:[A-Z][a-z]+\s*)?(?:[A-Z][a-z]+(?:-[A-Z][a-z]+)?)?(?:,\s*)?)+)*$", ErrorMessage = "Nieprawidłowy format gwiazd.")]
        public string? Stars { get; set; }//api

        //Rating that is not required but when given in the range of 1.0 - 10.0 will be included in the search
        [DisplayName("Ocena")]
        //Możliwe podanie liczby z zakresu 1.0 - 10.9 naprawic aby bylo 10.0
        [RegularExpression(@"^([1-9]|10)(\.\d)?$",ErrorMessage = "Nieprawidłowy format oceny")]
        public string? ImdbRating { get; set; }

        //Director is not required and is not included in the search
        [DisplayName("Reżyser")]
        public string? Director { get; set; }
    }
}
