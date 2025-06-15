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
    
    public class CzłonekZespołuController : Controller
    {
        private readonly AppDbContext _context;

        public CzłonekZespołuController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CzłonekZespołu
        public async Task<IActionResult> Index()
        {
            var czlonkowie = _context.CzlonkowieZespolu.ToList();
            return View(czlonkowie);
            //return View(await _context.CzlonkowieZespolu.ToListAsync());
        }

        // GET: CzłonekZespołu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var członekZespołu = await _context.CzlonkowieZespolu
                .FirstOrDefaultAsync(m => m.Id == id);
            if (członekZespołu == null)
            {
                return NotFound();
            }

            return View(członekZespołu);
        }

        // GET: CzłonekZespołu/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CzłonekZespołu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Imie,Nazwisko,Inicjaly,Telefon,CzyAdmin,Warstwa,CzyAudytor")] CzlonkowieZespolu członekZespołu)
        {
            try
            {
                var db = new AppDbContext();
                if (ModelState.IsValid)
                {
                    db.Add(członekZespołu);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(członekZespołu);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        // GET: CzłonekZespołu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var członekZespołu = await _context.CzlonkowieZespolu.FindAsync(id);
            if (członekZespołu == null)
            {
                return NotFound();
            }
            return View(członekZespołu);
        }

        // POST: CzłonekZespołu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Imie,Nazwisko,Inicjaly,Telefon,CzyAdmin,Warstwa,CzyAudytor")] CzlonkowieZespolu członekZespołu)
        {

            if (id != członekZespołu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var db = new AppDbContext();
                    db.Update(członekZespołu);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CzłonekZespołuExists(członekZespołu.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                catch (Exception ex)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(członekZespołu);

        }

        // GET: CzłonekZespołu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var członekZespołu = await _context.CzlonkowieZespolu
                .FirstOrDefaultAsync(m => m.Id == id);
            if (członekZespołu == null)
            {
                return NotFound();
            }

            return View(członekZespołu);
        }

        // POST: CzłonekZespołu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var db = new AppDbContext();
                var członekZespołu = await db.CzlonkowieZespolu.FindAsync(id);
                if (członekZespołu != null)
                {
                    db.CzlonkowieZespolu.Remove(członekZespołu);
                }

                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }

        }

        private bool CzłonekZespołuExists(int id)
        {
            return _context.CzlonkowieZespolu.Any(e => e.Id == id);
        }
    }
}
