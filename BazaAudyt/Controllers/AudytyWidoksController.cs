using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BazaAudyt.Models;
using Microsoft.AspNetCore.Authorization;

namespace BazaAudyt.Controllers
{
    
    public class AudytyWidoksController : Controller
    {
        private readonly AppDbContext _context;

        public AudytyWidoksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: AudytyWidoks
        public async Task<IActionResult> Index()
        {
            return View(await _context.AudytyWidok.ToListAsync());
        }

        // GET: AudytyWidoks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audytyWidok = await _context.AudytyWidok
                .FirstOrDefaultAsync(m => m.Id == id);
            if (audytyWidok == null)
            {
                return NotFound();
            }

            return View(audytyWidok);
        }

        // GET: AudytyWidoks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AudytyWidoks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AudytorId,Towarzyszacy,Data,Stanowisko,DataPlanowana,ObszarAudytu,DataZamkniecia,Pozycja,Lider,Wydzial,Brygada,Audytowany,Komentarz")] AudytyWidok audytyWidok)
        {
            if (ModelState.IsValid)
            {
                _context.Add(audytyWidok);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(audytyWidok);
        }

        // GET: AudytyWidoks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audytyWidok = await _context.AudytyWidok.FindAsync(id);
            if (audytyWidok == null)
            {
                return NotFound();
            }
            return View(audytyWidok);
        }

        // POST: AudytyWidoks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AudytorId,Towarzyszacy,Data,Stanowisko,DataPlanowana,ObszarAudytu,DataZamkniecia,Pozycja,Lider,Wydzial,Brygada,Audytowany,Komentarz")] AudytyWidok audytyWidok)
        {
            if (id != audytyWidok.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(audytyWidok);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AudytyWidokExists(audytyWidok.Id))
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
            return View(audytyWidok);
        }

        // GET: AudytyWidoks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audytyWidok = await _context.AudytyWidok
                .FirstOrDefaultAsync(m => m.Id == id);
            if (audytyWidok == null)
            {
                return NotFound();
            }

            return View(audytyWidok);
        }

        // POST: AudytyWidoks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var audytyWidok = await _context.AudytyWidok.FindAsync(id);
            if (audytyWidok != null)
            {
                _context.AudytyWidok.Remove(audytyWidok);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AudytyWidokExists(int id)
        {
            return _context.AudytyWidok.Any(e => e.Id == id);
        }
    }
}
