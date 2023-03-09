using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestProject.Data;
using TestProject.Models;

namespace TestProject.Controllers
{
    public class CompetenceController : Controller
    {
        private readonly DataContext _context;

        public CompetenceController(DataContext context)
        {
            _context = context;
        }

        // GET: Competence
        public async Task<IActionResult> Index()
        {
              return _context.Competences != null ? 
                          View(await _context.Competences.ToListAsync()) :
                          Problem("Entity set 'DataContext.Competences'  is null.");
        }

        // GET: Competence/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Competences == null)
            {
                return NotFound();
            }

            var competence = await _context.Competences
                .FirstOrDefaultAsync(m => m.CompetenceId == id);
            if (competence == null)
            {
                return NotFound();
            }

            return View(competence);
        }

        // GET: Competence/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Competence/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompetenceId,Name")] Competence competence)
        {
            if (ModelState.IsValid)
            {
                _context.Add(competence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(competence);
        }

        // GET: Competence/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Competences == null)
            {
                return NotFound();
            }

            var competence = await _context.Competences.FindAsync(id);
            if (competence == null)
            {
                return NotFound();
            }
            return View(competence);
        }

        // POST: Competence/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompetenceId,Name")] Competence competence)
        {
            if (id != competence.CompetenceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(competence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompetenceExists(competence.CompetenceId))
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
            return View(competence);
        }

        // GET: Competence/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Competences == null)
            {
                return NotFound();
            }

            var competence = await _context.Competences
                .FirstOrDefaultAsync(m => m.CompetenceId == id);
            if (competence == null)
            {
                return NotFound();
            }

            return View(competence);
        }

        // POST: Competence/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Competences == null)
            {
                return Problem("Entity set 'DataContext.Competences'  is null.");
            }
            var competence = await _context.Competences.FindAsync(id);
            if (competence != null)
            {
                _context.Competences.Remove(competence);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompetenceExists(int id)
        {
          return (_context.Competences?.Any(e => e.CompetenceId == id)).GetValueOrDefault();
        }
    }
}
