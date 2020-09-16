﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinique.Domain.Models;
using Clinique.EntityFramework;

namespace Clinique.AspNetCore.Controllers
{
    public class DossierpatientsController : Controller
    {
        private readonly CliniqueDbContext _context;

        public DossierpatientsController(CliniqueDbContext context)
        {
            _context = context;
        }

        // GET: Dossierpatients
        public async Task<IActionResult> Index()
        {
            var cliniqueDbContext = _context.Dossierpatients.Include(d => d.Docteur);
            return View(await cliniqueDbContext.ToListAsync());
        }

        // GET: Dossierpatients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dossierpatient = await _context.Dossierpatients
                .Include(d => d.Docteur)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dossierpatient == null)
            {
                return NotFound();
            }

            return View(dossierpatient);
        }

        // GET: Dossierpatients/Create
        public IActionResult Create()
        {
            ViewData["IdDocteur"] = new SelectList(_context.Docteurs, "Id", "Id");
            return View();
        }

        // POST: Dossierpatients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NomP,PrenomP,Genre,NumAS,DateNaiss,DateC,IdDocteur,Id")] Dossierpatient dossierpatient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dossierpatient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDocteur"] = new SelectList(_context.Docteurs, "Id", "Id", dossierpatient.IdDocteur);
            return View(dossierpatient);
        }

        // GET: Dossierpatients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dossierpatient = await _context.Dossierpatients.FindAsync(id);
            if (dossierpatient == null)
            {
                return NotFound();
            }
            ViewData["IdDocteur"] = new SelectList(_context.Docteurs, "Id", "Id", dossierpatient.IdDocteur);
            return View(dossierpatient);
        }

        // POST: Dossierpatients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NomP,PrenomP,Genre,NumAS,DateNaiss,DateC,IdDocteur,Id")] Dossierpatient dossierpatient)
        {
            if (id != dossierpatient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dossierpatient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DossierpatientExists(dossierpatient.Id))
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
            ViewData["IdDocteur"] = new SelectList(_context.Docteurs, "Id", "Id", dossierpatient.IdDocteur);
            return View(dossierpatient);
        }

        // GET: Dossierpatients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dossierpatient = await _context.Dossierpatients
                .Include(d => d.Docteur)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dossierpatient == null)
            {
                return NotFound();
            }

            return View(dossierpatient);
        }

        // POST: Dossierpatients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dossierpatient = await _context.Dossierpatients.FindAsync(id);
            _context.Dossierpatients.Remove(dossierpatient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DossierpatientExists(int id)
        {
            return _context.Dossierpatients.Any(e => e.Id == id);
        }
    }
}