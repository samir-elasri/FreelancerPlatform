using FreelancerPlatform.Data;
using FreelancerPlatform.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace FreelancerPlatform.Controllers
{
    public class RequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Requests
        [AdminOnly]
        public async Task<IActionResult> Index()
        {
            var requests = _context.Requests
                .Include(r => r.Freelancer)
                .Include(r => r.Project)
                .Where(r => !r.Approved);
            return View(await requests.ToListAsync());
        }

        [HttpGet]
        [FreelancerOnly]
        public IActionResult SendRequest(int projectId)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

            // create a new request
            Request request = new Request
            {
                FreelancerId = userId,
                ProjectId = projectId,
                Approved = false
            };

            // add the request to the database
            _context.Requests.Add(request);
            _context.SaveChanges();

            // redirect the user back to the projects index page
            return RedirectToAction("Index", "Projects");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminOnly]
        public async Task<IActionResult> Approve(int freelancerId, int projectId)
        {
            var request = await _context.Requests.FirstOrDefaultAsync(r => r.FreelancerId == freelancerId && r.ProjectId == projectId);
            if (request == null)
            {
                return NotFound();
            }
            request.Approved = true;
            _context.Requests.Update(request);

            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId);
            if (project != null)
            {
                project.FreelancerId = freelancerId;
                _context.Projects.Update(project);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Requests/Delete/5
        [AdminOnly]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Requests
                .Include(r => r.Freelancer)
                .Include(r => r.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

    }
}