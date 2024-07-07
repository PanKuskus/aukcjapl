using aukcjapl.Models;

namespace aukcjapl.Data.Uslugi
{
    public class UslugaKomentarz : IUslugaKomentarz
    {
        private readonly ApplicationDbContext _context;

        public UslugaKomentarz(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(Komentarz komentarz)
        {
            _context.Komentarze.Add(komentarz);
            await _context.SaveChangesAsync();
        }
    }
}
