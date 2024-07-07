using aukcjapl.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace aukcjapl.Data.Uslugi
{
    public class UslugaOferta : IUslugaOferta
    {

        
        private readonly ApplicationDbContext _context;

        public UslugaOferta(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Oferta oferta)
        {
            _context.Oferty.Add(oferta);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Oferta> GetAll()
        {
            var applicationDbContext = from a in _context.Oferty.Include(l => l.Lista).ThenInclude(l => l.Uzytkownik)
                                       select a;
            return applicationDbContext;
        }
    }
}
