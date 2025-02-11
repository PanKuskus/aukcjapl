using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aukcjapl.Models
{
    public class ListaVM
    {
        public int Id { get; set; }

      
        [Required(ErrorMessage = "Tytuł aukcji jest wymagany.")]
        public string Tytul { get; set; }

        [Required(ErrorMessage = "Opis przedmiotu jest wymagany.")]
        public string Opis { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Cena musi być liczbą większą od 0.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Cena musi być liczbą z dokładnością do dwóch miejsc po przecinku.")]
        public double Cena { get; set; }

        [Required(ErrorMessage = "Dodanie zdjęcia jest wymagane.")]
        public IFormFile Obraz { get; set; }
        public bool Sprzedane { get; set; }=false;

        [Required]
        public string? IdUzytkownika { get; set; }
        [ForeignKey("IdUzytkownika")]
        public IdentityUser? Uzytkownik { get; set; }
    }
}
