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

    public class PytanieController : Controller
    {
        private readonly AppDbContext _context;

        public PytanieController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Pytanie
        public async Task<IActionResult> Index()
        {
            if (AppDbContext.newConnectionString == "")
            {
                return RedirectToAction("Index", "Home");
            }
            return View(await _context.LPA_Pytania.ToListAsync());
        }

        // GET: Pytanie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (AppDbContext.newConnectionString == "")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var lPA_Pytanie = await _context.LPA_Pytania
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lPA_Pytanie == null)
            {
                return NotFound();
            }

            return View(lPA_Pytanie);
        }

        // GET: Pytanie/Create
        public IActionResult Create()
        {
            if (AppDbContext.newConnectionString == "")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: Pytanie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Pytanie,Obszar,Nr,Aktywne,Norma,Waga")] LPA_Pytania lPA_Pytanie)
        {
            if (AppDbContext.newConnectionString == "")
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                var db = new AppDbContext();
                if (ModelState.IsValid)
                {
                    db.Add(lPA_Pytanie);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(lPA_Pytanie);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Pytanie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (AppDbContext.newConnectionString == "")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var lPA_Pytanie = await _context.LPA_Pytania.FindAsync(id);
            if (lPA_Pytanie == null)
            {
                return NotFound();
            }
            return View(lPA_Pytanie);
        }

        // POST: Pytanie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Pytanie,Obszar,Nr,Aktywne,Norma,Waga")] LPA_Pytania lPA_Pytanie)
        {
            if (AppDbContext.newConnectionString == "")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id != lPA_Pytanie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var db = new AppDbContext();
                    db.Update(lPA_Pytanie);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LPA_PytanieExists(lPA_Pytanie.Id))
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
            return View(lPA_Pytanie);
        }

        // GET: Pytanie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (AppDbContext.newConnectionString == "")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var lPA_Pytanie = await _context.LPA_Pytania
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lPA_Pytanie == null)
            {
                return NotFound();
            }

            return View(lPA_Pytanie);
        }

        // POST: Pytanie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (AppDbContext.newConnectionString == "")
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {

                var db = new AppDbContext();
                var lPA_Pytanie = await db.LPA_Pytania.FindAsync(id);
                if (lPA_Pytanie != null)
                {
                    db.LPA_Pytania.Remove(lPA_Pytanie);
                }

                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
           }

        private bool LPA_PytanieExists(int id)
        {
            return _context.LPA_Pytania.Any(e => e.Id == id);
        }
    }
}
