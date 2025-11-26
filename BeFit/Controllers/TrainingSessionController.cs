using BeFit.Data;
using BeFit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BeFit.Controllers
{
    [Authorize]
    public class TrainingSessionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TrainingSessionController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: TrainingSession
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var sessions = await _context.TrainingSessions
                .Where(t => t.UserId == userId)
                .ToListAsync();
            return View(sessions);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var trainingSession = await _context.TrainingSessions
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

            if (trainingSession == null)
            {
                return NotFound();
            }

            return View(trainingSession);
        }

        // GET: TrainingSession/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TrainingSession/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StartTime,EndTime")] TrainingSession trainingSession)
        {
            if (ModelState.IsValid)
            {
                trainingSession.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(trainingSession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainingSession);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var trainingSession = await _context.TrainingSessions
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (trainingSession == null)
            {
                return NotFound();
            }
            return View(trainingSession);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartTime,EndTime")] TrainingSession trainingSession)
        {
            if (id != trainingSession.Id)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            // Ensure the user owns this session before updating
            var exists = await _context.TrainingSessions.AnyAsync(t => t.Id == id && t.UserId == userId);
            if (!exists)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Attach the UserId to the object being updated to ensure it doesn't get lost or changed
                    trainingSession.UserId = userId;
                    _context.Update(trainingSession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingSessionExists(trainingSession.Id))
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
            return View(trainingSession);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var trainingSession = await _context.TrainingSessions
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

            if (trainingSession == null)
            {
                return NotFound();
            }

            return View(trainingSession);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var trainingSession = await _context.TrainingSessions
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            
            if (trainingSession != null)
            {
                _context.TrainingSessions.Remove(trainingSession);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TrainingSessionExists(int id)
        {
            return _context.TrainingSessions.Any(e => e.Id == id);
        }
    }
}
