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
            if (ModelState.IsValid)
            {
                _context.Add(lPA_Wynik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lPA_Wynik);
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
                    _context.Update(lPA_Wynik);
                    await _context.SaveChangesAsync();
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
            var lPA_Wynik = await _context.LPA_Wyniki.FindAsync(id);
            if (lPA_Wynik != null)
            {
                _context.LPA_Wyniki.Remove(lPA_Wynik);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LPA_WynikExists(int id)
        {
            return _context.LPA_Wyniki.Any(e => e.Id == id);
        }
    }
}
