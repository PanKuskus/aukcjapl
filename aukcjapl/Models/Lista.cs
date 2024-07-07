using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aukcjapl.Models
{
    public class Lista
    {
        public int Id { get; set; }
        public string Tytul { get; set; }
        public string Opis { get; set; }
        public double Cena { get; set; }
        public string Obraz { get; set; }
        public bool Sprzedane { get; set; } = false;

        [Required]
        public string? IdUzytkownika { get; set; }
        [ForeignKey("IdUzytkownika")]
        public IdentityUser? Uzytkownik { get; set; }

        public List<Oferta>? Oferty {get; set;}
        public List<Komentarz>? Komentarz { get; set; }
    }
}
