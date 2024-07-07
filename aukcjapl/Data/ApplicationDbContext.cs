using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using aukcjapl.Models;



namespace aukcjapl.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Lista> Listy { get; set; }
        public DbSet<Oferta> Oferty { get; set; }
        public DbSet<Komentarz> Komentarze { get; set; }
    }
}
