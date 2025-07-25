﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BazaAudyt.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Authorization;

namespace BazaAudyt.Controllers
{
    
    public class AudytyController : Controller
    {
        private readonly AppDbContext _context;

        public AudytyController(AppDbContext context)
        {
            _context = context;
        }



        // GET: Audyty
        public async Task<IActionResult> Index()
        {
            if (AppDbContext.newConnectionString == "")
            {
                return RedirectToAction("Index", "Home");
            }
            using (SqlConnection connection = new SqlConnection(_context.loggedConnectionString))
            {
                var audyty = _context.LPA_PlanAudytow.ToList();
                //var audyty = _context.AudytyWidok.ToList();
                return View(audyty);
            }

        }

        // GET: Audyty/Details/5
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

            var audyt = await _context.LPA_PlanAudytow
                .FirstOrDefaultAsync(m => m.Id == id);
            if (audyt == null)
            {
                return NotFound();
            }

            return View(audyt);
        }

        // GET: Audyty/Create
        public IActionResult Create()
        {
            if (AppDbContext.newConnectionString == "")
            {
                return RedirectToAction("Index", "Home");
            }
            var czlonkowie = _context.CzlonkowieZespolu
                .Select(c => new SelectListItem
                {
                    Text = c.Imie + " " + c.Nazwisko,           
                    Value = c.Id.ToString()      
                }).ToList();

            ViewBag.Audytorzy = czlonkowie;

            return View();
        }

        // POST: Audyty/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AudytorId,Towarzyszacy,Data,Stanowisko,DataPlanowana,ObszarAudytu,DataZamkniecia,Pozycja,Lider,Wydzial,Brygada,Audytowany,Komentarz")] LPA_PlanAudytow audyt)
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
                    db.Add(audyt);
                    await db.SaveChangesAsync();

               /*     var pytania = db.LPA_Pytania
                        .Where(p => p.Obszar == audyt.ObszarAudytu.Trim())
                        .ToList();

                    foreach (var pytanie in pytania)
                    {
                        var wynik = new LPA_Wyniki
                        {
                            IdAudytu = audyt.Id,
                            Pytanie = pytanie.Id
                        };

                        db.LPA_Wyniki.Add(wynik);
                    }
               */
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(audyt);
            }
            catch (Exception ex)
            {
                //return NotFound();
               return RedirectToAction("Index");
            }
        }

        // GET: Audyty/Edit/5
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

                    var audyt = await _context.LPA_PlanAudytow.FindAsync(id);
                    if (audyt == null)
                    {
                        return NotFound();
                    }
                    return View(audyt);

        }

        // POST: Audyty/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AudytorId,Towarzyszacy,Data,Stanowisko,DataPlanowana,ObszarAudytu,DataZamkniecia,Pozycja,Lider,Wydzial,Brygada,Audytowany,Komentarz")] LPA_PlanAudytow audyt)
        {
            if (AppDbContext.newConnectionString == "")
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
               var db = new AppDbContext();
            //Do dokończenia
                if (id != audyt.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                    // _context.Update(audyt);
                    //await _context.SaveChangesAsync();
                    db.Update(audyt);
                    await db.SaveChangesAsync();
                    db.Dispose();
                }
                catch (DbUpdateConcurrencyException)
                    {
                        if (!AudytExists(audyt.Id))
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
                return View(audyt);
            }
            catch(Exception e)
            {
                
                return RedirectToAction("Index");
            }
            }


        // GET: Audyty/Delete/5
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

            var audyt = await _context.LPA_PlanAudytow
                .FirstOrDefaultAsync(m => m.Id == id);
            if (audyt == null)
            {
                return NotFound();
            }

            return View(audyt);
        }

        // POST: Audyty/Delete/5
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
                var audyt = await _context.LPA_PlanAudytow.FindAsync(id);
                if (audyt != null)
                {
                    _context.LPA_PlanAudytow.Remove(audyt);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }

        }

        private bool AudytExists(int id)
        {
            return _context.LPA_PlanAudytow.Any(e => e.Id == id);
        }
        [HttpPost]
        public ActionResult GenerujRaport()
        {
            if (AppDbContext.newConnectionString == "")
            {
                return RedirectToAction("Index", "Home");
            }
            string projectRoot = AppDomain.CurrentDomain.BaseDirectory;
            string exePath = Path.Combine(projectRoot, "raport.exe");

            if (!System.IO.File.Exists(exePath))
                return Content("Plik EXE nie istnieje: " + exePath);

            var psi = new ProcessStartInfo
            {
                FileName = exePath,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            try
            {
                using (var process = Process.Start(psi))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    if (!string.IsNullOrWhiteSpace(error))
                        return RedirectToAction("Index");

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }
    }
}
