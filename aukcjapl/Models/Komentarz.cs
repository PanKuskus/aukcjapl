using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace aukcjapl.Models
{
    public class Komentarz
    {
        public int Id { get; set; }
        public string Tekst { get; set; }

        [Required]
        public string? IdUzytkownika { get; set; }
        [ForeignKey("IdUzytkownika")]
        public IdentityUser? Uzytkownik { get; set; }

        public int? IdListy { get; set; }
        [ForeignKey("IdListy")]
        public Lista? Lista { get; set; }
    }
}
