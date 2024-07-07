using aukcjapl.Models;

namespace aukcjapl.Data.Uslugi
{
    public interface IUslugaListy
    {
        IQueryable<Lista> GetAll();
        Task Add(Lista lista);
        Task<Lista> GetById(int? id);

        Task SaveChanges();
    }
}
