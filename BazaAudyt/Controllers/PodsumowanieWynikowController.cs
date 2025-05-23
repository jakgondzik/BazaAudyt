using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BazaAudyt.Models;

namespace BazaAudyt.Controllers
{
    public class PodsumowanieWynikowController : Controller
    {
        private readonly AppDbContext _context;

        public PodsumowanieWynikowController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PodsumowanieWynikow
        public async Task<IActionResult> Index()
        {
            return View(await _context.LPA_PodsumowanieWynikow.ToListAsync());
        }

        // GET: PodsumowanieWynikow/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var podsumowanieWyniku = await _context.LPA_PodsumowanieWynikow
                .FirstOrDefaultAsync(m => m.Id == id);
            if (podsumowanieWyniku == null)
            {
                return NotFound();
            }

            return View(podsumowanieWyniku);
        }

        // GET: PodsumowanieWynikow/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PodsumowanieWynikow/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdCzesci,DataWykonania,IdAudytowanego,IdAudytu,Komentarz,Lider,Audytowany,Rozpoczety")] PodsumowanieWyniku podsumowanieWyniku)
        {
            if (ModelState.IsValid)
            {
                _context.Add(podsumowanieWyniku);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(podsumowanieWyniku);
        }

        // GET: PodsumowanieWynikow/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var podsumowanieWyniku = await _context.LPA_PodsumowanieWynikow.FindAsync(id);
            if (podsumowanieWyniku == null)
            {
                return NotFound();
            }
            return View(podsumowanieWyniku);
        }

        // POST: PodsumowanieWynikow/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdCzesci,DataWykonania,IdAudytowanego,IdAudytu,Komentarz,Lider,Audytowany,Rozpoczety")] PodsumowanieWyniku podsumowanieWyniku)
        {
            if (id != podsumowanieWyniku.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(podsumowanieWyniku);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PodsumowanieWynikuExists(podsumowanieWyniku.Id))
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
            return View(podsumowanieWyniku);
        }

        // GET: PodsumowanieWynikow/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var podsumowanieWyniku = await _context.LPA_PodsumowanieWynikow
                .FirstOrDefaultAsync(m => m.Id == id);
            if (podsumowanieWyniku == null)
            {
                return NotFound();
            }

            return View(podsumowanieWyniku);
        }

        // POST: PodsumowanieWynikow/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var podsumowanieWyniku = await _context.LPA_PodsumowanieWynikow.FindAsync(id);
            if (podsumowanieWyniku != null)
            {
                _context.LPA_PodsumowanieWynikow.Remove(podsumowanieWyniku);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PodsumowanieWynikuExists(int id)
        {
            return _context.LPA_PodsumowanieWynikow.Any(e => e.Id == id);
        }
    }
}
