using aukcjapl.Models;

namespace aukcjapl.Data.Uslugi
{
    public interface IUslugaKomentarz
    {
        Task Add(Komentarz komentarz);
    }
}
