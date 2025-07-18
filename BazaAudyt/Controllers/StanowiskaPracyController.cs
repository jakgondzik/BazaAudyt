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

    public class StanowiskaPracyController : Controller
    {
        private readonly AppDbContext _context;

        public StanowiskaPracyController(AppDbContext context)
        {
            _context = context;
        }

        // GET: StanowiskaPracy
        public async Task<IActionResult> Index()
        {
            return View(await _context.StanowiskaPracy.ToListAsync());
        }

        // GET: StanowiskaPracy/Details/5
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

            var stanowiskoPracy = await _context.StanowiskaPracy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stanowiskoPracy == null)
            {
                return NotFound();
            }

            return View(stanowiskoPracy);
        }

        // GET: StanowiskaPracy/Create
        public IActionResult Create()
        {
            if (AppDbContext.newConnectionString == "")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: StanowiskaPracy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Wydzial,Proces,Gniazdo,NrGniazda,RodzajStanowiska,IdLidera,Typ,ObszarLPA")] StanowiskoPracy stanowiskoPracy)
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
                    db.Add(stanowiskoPracy);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(stanowiskoPracy);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }

        }

        // GET: StanowiskaPracy/Edit/5
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

            var stanowiskoPracy = await _context.StanowiskaPracy.FindAsync(id);
            if (stanowiskoPracy == null)
            {
                return NotFound();
            }
            return View(stanowiskoPracy);
        }

        // POST: StanowiskaPracy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Wydzial,Proces,Gniazdo,NrGniazda,RodzajStanowiska,IdLidera,Typ,ObszarLPA")] StanowiskoPracy stanowiskoPracy)
        {
            if (AppDbContext.newConnectionString == "")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id != stanowiskoPracy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var db = new AppDbContext();
                    db.Update(stanowiskoPracy);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StanowiskoPracyExists(stanowiskoPracy.Id))
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
                }
                return RedirectToAction(nameof(Index));
            }
            return View(stanowiskoPracy);
        }

        // GET: StanowiskaPracy/Delete/5
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

            var stanowiskoPracy = await _context.StanowiskaPracy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stanowiskoPracy == null)
            {
                return NotFound();
            }

            return View(stanowiskoPracy);
        }

        // POST: StanowiskaPracy/Delete/5
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
                var stanowiskoPracy = await db.StanowiskaPracy.FindAsync(id);
                if (stanowiskoPracy != null)
                {
                    db.StanowiskaPracy.Remove(stanowiskoPracy);
                }

                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex) {
                return RedirectToAction("Index");
            }


        }

        private bool StanowiskoPracyExists(int id)
        {
            return _context.StanowiskaPracy.Any(e => e.Id == id);
        }
    }
}
