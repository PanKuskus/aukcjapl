using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using aukcjapl.Data;
using aukcjapl.Models;
using aukcjapl.Data.Uslugi;
using Microsoft.AspNetCore.Hosting;
using System.Net.WebSockets;
using Microsoft.Build.Framework;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;

namespace aukcjapl.Controllers
{
    public class ListaController : Controller
    {
        private readonly IUslugaListy _uslugaListy;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUslugaOferta _uslugaOferta;
        private readonly IUslugaKomentarz _uslugaKomentarz;
        public ListaController(IUslugaListy uslugaListy, IWebHostEnvironment webHostEnvironment, IUslugaOferta uslugaOferta, IUslugaKomentarz uslugaKomentarz)
        {
            _uslugaListy = uslugaListy;
            _webHostEnvironment = webHostEnvironment;
            _uslugaOferta = uslugaOferta;
            _uslugaKomentarz = uslugaKomentarz;

        }

        // GET: Lista
        public async Task<IActionResult> Index(int? pageNumber, string searchString)
        {
            var applicationDbContext = _uslugaListy.GetAll();
            int pageSize = 10;
            if (!string.IsNullOrEmpty(searchString))
            {
                applicationDbContext = applicationDbContext.Where(a => a.Tytul.Contains(searchString));
                return View(await PaginatedList<Lista>.CreateAsync(applicationDbContext.Where(l => l.Sprzedane == false).AsNoTracking(), pageNumber ?? 1, pageSize));
            }

            return View(await PaginatedList<Lista>.CreateAsync(applicationDbContext.Where(l => l.Sprzedane == false).AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        public async Task<IActionResult> MyListings(int? pageNumber)
        {
            var applicationDbContext = _uslugaListy.GetAll();
            int pageSize = 5;
            return View("Index", await PaginatedList<Lista>.CreateAsync(applicationDbContext.Where(l => l.IdUzytkownika == User.FindFirstValue(ClaimTypes.NameIdentifier)).AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> MyBids(int? pageNumber)
        {
            var applicationDbContext = _uslugaOferta.GetAll();
            int pageSize = 3;

            return View(await PaginatedList<Oferta>.CreateAsync(applicationDbContext.Where(l => l.IdUzytkownika == User.FindFirstValue(ClaimTypes.NameIdentifier)).AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Lista/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lista = await _uslugaListy.GetById(id);

            if (lista == null)
            {
                return NotFound();
            }

            return View(lista);
        }
        
        // GET: Lista/Create
        public IActionResult Create()
        {
            
            return View();
        }
        
        // POST: Lista/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ListaVM lista)
        {
            if(lista.Obraz != null)
            {
                string sciezkaDoObrazow = Path.Combine(_webHostEnvironment.WebRootPath, "Obrazy");
                string nazwaPliku = lista.Obraz.FileName;
                string sciezka = Path.Combine(sciezkaDoObrazow, nazwaPliku);
                using (var plik = new FileStream(sciezka, FileMode.Create))
                {
                    lista.Obraz.CopyTo(plik);
                }

                var listaObj = new Lista
                {
                    Tytul = lista.Tytul,
                    Opis = lista.Opis,
                    Cena = lista.Cena,
                    IdUzytkownika = lista.IdUzytkownika,
                    Obraz = nazwaPliku
                };
                await _uslugaListy.Add(listaObj);
                return RedirectToAction("Index");
            }
            return View(lista);
        }


        [HttpPost]
        public async Task<ActionResult> AddBid([Bind("Id, Cena, IdListy, IdUzytkownika")] Oferta oferta)
        {
            if(ModelState.IsValid)
            {
                await _uslugaOferta.Add(oferta);
            }
            var lista = await _uslugaListy.GetById(oferta.IdListy);
            lista.Cena = oferta.Cena;
            await _uslugaListy.SaveChanges();
            
            return View("Details", lista);
        }

        public async Task<ActionResult> CloseBidding(int id)
        {
            var lista = await _uslugaListy.GetById(id);
            lista.Sprzedane = true;
            await _uslugaListy.SaveChanges();
            return View("Details", lista);
        }

        public async Task<ActionResult> AddComment([Bind("Id, Tekst, IdListy, IdUzytkownika")] Komentarz komentarz)
        {
            if(ModelState.IsValid)
            {
                await _uslugaKomentarz.Add(komentarz);

            }
            var lista = await _uslugaListy.GetById(komentarz.IdListy);
            return View("Details", lista);
        }
       /*
       
       // GET: Lista/Edit/5
       public async Task<IActionResult> Edit(int? id)
       {
           if (id == null)
           {
               return NotFound();
           }

           var lista = await _context.Listy.FindAsync(id);
           if (lista == null)
           {
               return NotFound();
           }
           ViewData["IdUzytkownika"] = new SelectList(_context.Users, "Id", "Id", lista.IdUzytkownika);
           return View(lista);
       }

       // POST: Lista/Edit/5
       // To protect from overposting attacks, enable the specific properties you want to bind to.
       // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       [HttpPost]
       [ValidateAntiForgeryToken]
       public async Task<IActionResult> Edit(int id, [Bind("Id,Tytul,Opis,Cena,Obraz,Sprzedane,IdUzytkownika")] Lista lista)
       {
           if (id != lista.Id)
           {
               return NotFound();
           }

           if (ModelState.IsValid)
           {
               try
               {
                   _context.Update(lista);
                   await _context.SaveChangesAsync();
               }
               catch (DbUpdateConcurrencyException)
               {
                   if (!ListaExists(lista.Id))
                   {
                       return NotFound();
                   }
                   else
                   {
                       throw;
                   }
               }
               return RedirectToAction(nameof(Index));
           }
           ViewData["IdUzytkownika"] = new SelectList(_context.Users, "Id", "Id", lista.IdUzytkownika);
           return View(lista);
       }

       // GET: Lista/Delete/5
       public async Task<IActionResult> Delete(int? id)
       {
           if (id == null)
           {
               return NotFound();
           }

           var lista = await _context.Listy
               .Include(l => l.Uzytkownik)
               .FirstOrDefaultAsync(m => m.Id == id);
           if (lista == null)
           {
               return NotFound();
           }

           return View(lista);
       }

       // POST: Lista/Delete/5
       [HttpPost, ActionName("Delete")]
       [ValidateAntiForgeryToken]
       public async Task<IActionResult> DeleteConfirmed(int id)
       {
           var lista = await _context.Listy.FindAsync(id);
           if (lista != null)
           {
               _context.Listy.Remove(lista);
           }

           await _context.SaveChangesAsync();
           return RedirectToAction(nameof(Index));
       }

       private bool ListaExists(int id)
       {
           return _context.Listy.Any(e => e.Id == id);
       }
       */
    }
}
