namespace aukcjapl.Models;

public interface IUslugaOferta
{
    Task Add(Oferta oferta);
    IQueryable<Oferta> GetAll();
}
