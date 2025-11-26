using BeFit.Data;
using BeFit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BeFit.Controllers
{
    [Authorize]
    public class ExerciseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExerciseController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var exercises = await _context.Exercises
                .Include(e => e.ExerciseType)
                .Include(e => e.TrainingSession)
                .Where(e => e.TrainingSession!.UserId == userId)
                .ToListAsync();

            return View(exercises);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var exercise = await _context.Exercises
                .Include(e => e.ExerciseType)
                .Include(e => e.TrainingSession)
                .FirstOrDefaultAsync(m => m.Id == id && m.TrainingSession!.UserId == userId);

            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // GET: Exercise/Create
        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            // Only show sessions belonging to the current user
            ViewData["TrainingSessionId"] = new SelectList(
                _context.TrainingSessions
                    .Where(ts => ts.UserId == userId)
                    .Select(ts => new
                    {
                        ts.Id,
                        Display = ts.Name + " — " + ts.StartTime.ToString("dd.MM.yyyy HH:mm")
                    }),
                "Id",
                "Display"
            );

            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseType, "Id", "Name");
            return View();
        }

        // POST: Exercise/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TrainingSessionId,ExerciseTypeId,Load,Sets,Repetitions")] Exercise exercise)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Verify that the selected session belongs to the user
            var session = await _context.TrainingSessions.FirstOrDefaultAsync(ts => ts.Id == exercise.TrainingSessionId && ts.UserId == userId);
            if (session == null)
            {
                ModelState.AddModelError("TrainingSessionId", "Invalid Training Session.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(exercise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["TrainingSessionId"] = new SelectList(
                _context.TrainingSessions
                    .Where(ts => ts.UserId == userId)
                    .Select(ts => new
                    {
                        ts.Id,
                        Display = ts.Name + " — " + ts.StartTime.ToString("dd.MM.yyyy HH:mm")
                    }),
                "Id",
                "Display",
                exercise.TrainingSessionId
            );

            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseType, "Id", "Name", exercise.ExerciseTypeId);
            return View(exercise);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var exercise = await _context.Exercises
                .Include(e => e.TrainingSession)
                .FirstOrDefaultAsync(e => e.Id == id && e.TrainingSession!.UserId == userId);

            if (exercise == null)
            {
                return NotFound();
            }

            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseType, "Id", "Name", exercise.ExerciseTypeId);
            
            ViewData["TrainingSessionId"] = new SelectList(
                _context.TrainingSessions
                    .Where(ts => ts.UserId == userId)
                    .Select(ts => new
                    {
                        ts.Id,
                        Display = ts.Name + " — " + ts.StartTime.ToString("dd.MM.yyyy HH:mm")
                    }),
                "Id",
                "Display",
                exercise.TrainingSessionId
            );
            
            return View(exercise);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TrainingSessionId,ExerciseTypeId,Load,Sets,Repetitions")] Exercise exercise)
        {
            if (id != exercise.Id)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var session = await _context.TrainingSessions.FirstOrDefaultAsync(ts => ts.Id == exercise.TrainingSessionId && ts.UserId == userId);
            if (session == null)
            {
                return NotFound(); // Or add model error
            }

            var exists = await _context.Exercises.Include(e => e.TrainingSession).AnyAsync(e => e.Id == id && e.TrainingSession!.UserId == userId);
            if (!exists)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exercise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerciseExists(exercise.Id))
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
            
            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseType, "Id", "Name", exercise.ExerciseTypeId);
            ViewData["TrainingSessionId"] = new SelectList(
                _context.TrainingSessions
                    .Where(ts => ts.UserId == userId)
                    .Select(ts => new
                    {
                        ts.Id,
                        Display = ts.Name + " — " + ts.StartTime.ToString("dd.MM.yyyy HH:mm")
                    }),
                "Id",
                "Display",
                exercise.TrainingSessionId
            );
            return View(exercise);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var exercise = await _context.Exercises
                .Include(e => e.ExerciseType)
                .Include(e => e.TrainingSession)
                .FirstOrDefaultAsync(m => m.Id == id && m.TrainingSession!.UserId == userId);

            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var exercise = await _context.Exercises
                .Include(e => e.TrainingSession)
                .FirstOrDefaultAsync(e => e.Id == id && e.TrainingSession!.UserId == userId);

            if (exercise != null)
            {
                _context.Exercises.Remove(exercise);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseExists(int id)
        {
            return _context.Exercises.Any(e => e.Id == id);
        }
    }
}
