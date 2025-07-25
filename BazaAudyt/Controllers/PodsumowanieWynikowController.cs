﻿using System;
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
        public async Task<IActionResult> Create([Bind("Id,IdCzesci,DataWykonania,IdAudytowanego,IdAudytu,Komentarz,Lider,Audytowany,Rozpoczety")] LPA_PodsumowanieWynikow podsumowanieWyniku)
        {
            try
            {
                var db = new AppDbContext();
            
            if (ModelState.IsValid)
            {
                    db.Add(podsumowanieWyniku);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(podsumowanieWyniku);
        }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdCzesci,DataWykonania,IdAudytowanego,IdAudytu,Komentarz,Lider,Audytowany,Rozpoczety")] LPA_PodsumowanieWynikow podsumowanieWyniku)
        {

            if (id != podsumowanieWyniku.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var db = new AppDbContext();
                    db.Update(podsumowanieWyniku);
                    await db.SaveChangesAsync();
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
                catch (Exception ex) {
                    return RedirectToAction("Index");
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
            try
            {
                var db = new AppDbContext();
                var podsumowanieWyniku = await db.LPA_PodsumowanieWynikow.FindAsync(id);
            if (podsumowanieWyniku != null)
            {
                    db.LPA_PodsumowanieWynikow.Remove(podsumowanieWyniku);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        private bool PodsumowanieWynikuExists(int id)
        {
            return _context.LPA_PodsumowanieWynikow.Any(e => e.Id == id);
        }
    }
}
