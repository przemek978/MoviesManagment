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
        //Możliwe podanie gatunków po przecinku które zawierja myslnik np. Sci-fi, cyfry ani spacje niedozowolone
        [RegularExpression(@"^[A-Za-z]+(?:-[A-Za-z]+)?(?:,\s*[A-Za-z]+(?:-[A-Za-z]+)?)*$", ErrorMessage = "Nieprawidłowy format gatunków.")]
        public string? Genres { get; set; }//api

        [DisplayName("Gwiazdy")]
        //Możliwe podawanie imion i nazwisk w roznych formatach do dwoch imion i nazwisk z myslnikami lub spacjami, cyfry niedozowolne
        [RegularExpression(@"^(?:[A-Z][a-z]+\s+(?:[A-Z][a-z]+\s*)?(?:[A-Z][a-z]+(?:-[A-Z][a-z]+)?)?(?:,\s*)?)+(?:,\s*(?:[A-Z][a-z]+\s+(?:[A-Z][a-z]+\s*)?(?:[A-Z][a-z]+(?:-[A-Z][a-z]+)?)?(?:,\s*)?)+)*$", ErrorMessage = "Nieprawidłowy format gwiazd.")]
        public string? Stars { get; set; }//api

        [DisplayName("Ocena")]
        //Możliwe podanie liczby z zakresu 1.0 - 10.9 naprawic aby bylo 10.0
        [RegularExpression(@"^([1-9]|10)(\.\d)?$",ErrorMessage = "Nieprawidłowy format oceny")]
        public string? ImdbRating { get; set; }

    }
}
