using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BazaAudyt.Models;
using BazaAudyt.Models.ViewModels;

namespace BazaAudyt.Controllers
{

    public class WynikController : Controller
    {
        private readonly AppDbContext _context;

        public WynikController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Wynik
        public async Task<IActionResult> Index()
        {
            return View(await _context.LPA_Wyniki.ToListAsync());
        }

        // GET: Wynik/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lPA_Wynik = await _context.LPA_Wyniki
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lPA_Wynik == null)
            {
                return NotFound();
            }

            return View(lPA_Wynik);
        }

        // GET: Wynik/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Wynik/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Pytanie,Wynik,IdAudytu,Komentarz,Wartosc,Uwagi")] LPA_Wyniki lPA_Wynik)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(lPA_Wynik);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(lPA_Wynik);
            }
            catch (Exception ex) {
                return RedirectToAction("Index");
            }

        }

        // GET: Wynik/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lPA_Wynik = await _context.LPA_Wyniki.FindAsync(id);
            if (lPA_Wynik == null)
            {
                return NotFound();
            }
            return View(lPA_Wynik);
        }

        // POST: Wynik/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Pytanie,Wynik,IdAudytu,Komentarz,Wartosc,Uwagi")] LPA_Wyniki lPA_Wynik)
        {
            if (id != lPA_Wynik.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var db = new AppDbContext();
                    db.Update(lPA_Wynik);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LPA_WynikExists(lPA_Wynik.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex) {
                    return RedirectToAction("Index");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(lPA_Wynik);
        }

        // GET: Wynik/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lPA_Wynik = await _context.LPA_Wyniki
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lPA_Wynik == null)
            {
                return NotFound();
            }

            return View(lPA_Wynik);
        }

        // POST: Wynik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var db = new AppDbContext();
                var lPA_Wynik = await db.LPA_Wyniki.FindAsync(id);
                if (lPA_Wynik != null)
                {
                   db.LPA_Wyniki.Remove(lPA_Wynik);
                }

                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex) {
                return RedirectToAction("Index");
            }

        }

        private bool LPA_WynikExists(int id)
        {
            return _context.LPA_Wyniki.Any(e => e.Id == id);
        }



        public IActionResult FormularzWynikow(int audytId)
        {
            var audyt = _context.LPA_PlanAudytow.FirstOrDefault(a => a.Id == audytId);
            if (audyt == null)
                return NotFound();
            Console.WriteLine("audyt.ObszarAudytu = " + audyt.ObszarAudytu);


            var pytania = _context.LPA_Pytania
    .Where(p => p.Obszar.Trim().ToLower() == audyt.ObszarAudytu.Trim().ToLower())
    .ToList();
            var wszystkieObszary = _context.LPA_Pytania.Select(p => p.Obszar).Distinct().ToList();
            foreach (var o in wszystkieObszary)
            {
                Console.WriteLine("Obszar w pytaniu: '" + o + "'");
            }


            var wyniki = _context.LPA_Wyniki
                .Where(w => w.IdAudytu == audytId)
                .ToList();

            var model = pytania
                .GroupJoin(wyniki, p => p.Id, w => w.Pytanie, (p, wynikiGroup) => new { p, wynik = wynikiGroup.FirstOrDefault() })
                .Select(x => new PytanieZwynikiemViewModel
                {
                    PytanieId = x.p.Id,
                    PytanieTresc = x.p.Pytanie,
                    WynikId = x.wynik?.Id,
                    Wynik = x.wynik?.Wynik,
                    Komentarz = x.wynik?.Komentarz,
                    Wartosc = x.wynik?.Wartosc,
                    Uwagi = x.wynik?.Uwagi,
                    AudytId = audytId
                }).ToList();

            return View(model);
        }


        [HttpPost]
        public IActionResult FormularzWynikow(List<PytanieZwynikiemViewModel> model)
        {
            foreach (var x in model)
            {
                LPA_Wyniki wynik;

                if (x.WynikId.HasValue)
                {
                    wynik = _context.LPA_Wyniki.First(w => w.Id == x.WynikId);
                }
                else
                {
                    wynik = new LPA_Wyniki
                    {
                        IdAudytu = x.AudytId,
                        Pytanie = x.PytanieId
                    };
                    _context.LPA_Wyniki.Add(wynik);
                }

                wynik.Wynik = x.Wynik;
                wynik.Komentarz = x.Komentarz;
                wynik.Wartosc = x.Wartosc;
                wynik.Uwagi = x.Uwagi;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Audyty");
        }

    }
}
