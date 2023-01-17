using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FreelancerPlatform.Data;
using FreelancerPlatform.Models;

namespace FreelancerPlatform.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Projects.Include(p => p.Category).Include(p => p.Freelancer);
            return View(await applicationDbContext.ToListAsync());
        }


		public async Task<IActionResult> Index2()
		{
			var applicationDbContext = _context.Projects.Include(p => p.Category).Include(p => p.Freelancer);
			return View(await applicationDbContext.ToListAsync());
		}

		// GET: Projects/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Category)
                .Include(p => p.Freelancer)
                .Include(p => p.Tasks)
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // Function to add comments to a project in its Details view:
        [HttpPost]
        public async Task<IActionResult> CreateComment(IFormCollection form)
        {
            int Id = Int32.Parse(form["ProjectId"]);
            var project = await _context.Projects.FindAsync(Id);
            if (project != null)
            {
                Comment comment = new Comment();
                comment.Text = form["Text"];
                comment.Project = project;

                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = Id });
            }
            return NotFound();
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["FreelancerId"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }


        // search view
        public async Task<IActionResult> Search()
        {
            return View();
        }
        public async Task<IActionResult> SearchResults(String searchData)
        {
            return View("Index",
            await _context.Projects.Where(i => i.Title.Contains(searchData)).ToListAsync());
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Title,Description,CreatedDate,FreelancerId,CategoryId")] Project project, List<Models.Task> tasks/*, IFormCollection form*/)
        {
            //if (ModelState.IsValid)
            //{
            project.CreatedDate = DateTime.Now;
            project.Freelancer = null;
            project.FreelancerId = null;

            _context.Add(project);
            await _context.SaveChangesAsync();

            var projectId = project.Id;
            for (var i = 0; i < tasks.Count; i++)
            {
                tasks[i].ProjectId = projectId;
                _context.Tasks.Add(tasks[i]);
            }
            //_context.Tasks.AddRange(tasks);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
            //}
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", project.CategoryId);
            ViewData["FreelancerId"] = new SelectList(_context.Users, "Id", "Email", project.FreelancerId);
            return View(project);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", project.CategoryId);
            ViewData["FreelancerId"] = new SelectList(_context.Users, "Id", "Email", project.FreelancerId);
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Title,Description,CreatedDate,FreelancerId,CategoryId")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", project.CategoryId);
            ViewData["FreelancerId"] = new SelectList(_context.Users, "Id", "Email", project.FreelancerId);
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Category)
                .Include(p => p.Freelancer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Projects == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Projects'  is null.");
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
          return _context.Projects.Any(e => e.Id == id);
        }
    }
}
