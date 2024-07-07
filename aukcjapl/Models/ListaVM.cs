using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aukcjapl.Models
{
    public class ListaVM
    {
        public int Id { get; set; }
        public string Tytul { get; set; }
        public string Opis { get; set; }
        public double Cena { get; set; }
        public IFormFile Obraz { get; set; }
        public bool Sprzedane { get; set; }=false;

        [Required]
        public string? IdUzytkownika { get; set; }
        [ForeignKey("IdUzytkownika")]
        public IdentityUser? Uzytkownik { get; set; }
    }
}
