using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestProject.Data;
using TestProject.Models;
using TestProject.ViewModels;

namespace TestProject.Controllers
{
    public class ProjectController : Controller
    {
        private readonly DataContext _context;

        public ProjectController(DataContext context)
        {
            _context = context;
        }

        // GET: Project
        public async Task<IActionResult> Index()
        {
            var viewModel = new ProjectViewModel();
            viewModel.Projects = _context.Projects
                .Include(p => p.Competences);

            return View(viewModel);
        }

        // GET: Project/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Competences)
                .FirstOrDefaultAsync(m => m.ProjectId == id);

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Project/Create
        public IActionResult Create()
        {
            var project = new Project();
            project.Competences = new List<Competence>();
            ShowCompInProject(project);
            return View();
        }

        // POST: Project/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectId,Title,Published")] Project project, string[] selectedCompetences)
        {

            if (selectedCompetences != null)
            {
                project.Competences = new List<Competence>();
                foreach (var competence in selectedCompetences)
                {
                    var competenceToAdd = _context.Competences.Find(int.Parse(competence));
                    project.Competences.Add(competenceToAdd);
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Project/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            Project project = _context.Projects
             .Include(p => p.Competences)
             .Where(p => p.ProjectId == id)
             .Single();

            //var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            ShowCompInProject(project);

            return View(project);
        }

        // POST: Project/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectId,Title,Published")] Project project, string[] selectedCompetences)
        {
            if (id != project.ProjectId)
            {
                return NotFound();
            }

            var projectToUpdate = _context.Projects
                 .Include(p => p.Competences)
                 .Where(p => p.ProjectId == id)
                 .Single();

            if (ModelState.IsValid)
            {
                try
                {
                    // Kod från tutorial
                    UpdateProjectComptences(selectedCompetences, projectToUpdate);
                    //_context.SaveChanges();

                    // Scaffoldad kod
                    _context.Update(projectToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectId))
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
            ShowCompInProject(projectToUpdate);
            return View(projectToUpdate);
        }

        // GET: Project/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Projects == null)
            {
                return Problem("Entity set 'DataContext.Projects'  is null.");
            }
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return (_context.Projects?.Any(e => e.ProjectId == id)).GetValueOrDefault();
        }

        private void ShowCompInProject(Project project)
        {
            var allCompetences = _context.Competences;
            var projectCompetences = new HashSet<int>(project.Competences.Select(c => c.CompetenceId));
            var viewModel = new List<CompInProject>();
            foreach (var competence in allCompetences)
            {
                viewModel.Add(new CompInProject
                {
                    CompetenceId = competence.CompetenceId,
                    Name = competence.Name,
                    IsChecked = projectCompetences.Contains(competence.CompetenceId)
                });
            }
            ViewBag.Competences = viewModel;
        }

        private void UpdateProjectComptences(string[] selectedCompetences, Project projectToUpdate)
        {
            if (selectedCompetences == null)
            {
                projectToUpdate.Competences = new List<Competence>();
                return;
            }

            var selectedCompetencesHS = new HashSet<string>(selectedCompetences);
            var projectCompetences = new HashSet<int>
                (projectToUpdate.Competences.Select(c => c.CompetenceId));

            foreach (var competence in projectToUpdate.Competences)
            {
                if (selectedCompetencesHS.Contains(competence.CompetenceId.ToString()))
                {
                    if (!projectCompetences.Contains(competence.CompetenceId))
                    {
                        projectToUpdate.Competences.Add(competence);
                    }
                }
                else
                {
                    if (projectCompetences.Contains(competence.CompetenceId))
                    {
                        projectToUpdate.Competences.Remove(competence);
                    }
                }
            }
        }
    }

}
