using aukcjapl.Models;
using Microsoft.EntityFrameworkCore;

namespace aukcjapl.Data.Uslugi
{
    public class UslugaListy : IUslugaListy
    {
        private readonly ApplicationDbContext _context;

        public UslugaListy(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Lista lista)
        {
            _context.Listy.Add(lista);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Lista> GetAll()
        {
            var applicationDbContext = _context.Listy.Include(l => l.Uzytkownik);
            return applicationDbContext;
        }

        public async Task<Lista> GetById(int? id)
        {
            var lista = await _context.Listy
               .Include(l => l.Uzytkownik)
               .Include(l => l.Komentarz)
               .Include(l => l.Oferty)
               .ThenInclude(l => l.Uzytkownik)
               .FirstOrDefaultAsync(m => m.Id == id);
            return lista;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
