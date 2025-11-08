using BeFit.Data;
using BeFit.Models;
using BeFit.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeFit.Controllers
{
    public class ExercisePerformedController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExercisePerformedController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var exercises = await _context.ExercisePerformed
        .Include(e => e.ExerciseType)
        .Include(e => e.TrainingSession)
        .Select(e => new ExercisePerformedDTO
        {
            Id = e.Id,
            TrainingSessionId = e.TrainingSessionId,
            ExerciseTypeId = e.ExerciseTypeId,
            Load = e.Load,
            Sets = e.Sets,
            Repetitions = e.Repetitions,
            ExerciseTypeName = e.ExerciseType.Name,
            TrainingSessionName = e.TrainingSession.Name
        })
        .ToListAsync();

            return View(exercises);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercisePerformed = await _context.ExercisePerformed
                .Include(e => e.ExerciseType)
                .Include(e => e.TrainingSession)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exercisePerformed == null)
            {
                return NotFound();
            }

            return View(exercisePerformed);
        }

        public IActionResult Create()
        {
            var model = new ExercisePerformed();

            ViewData["TrainingSessionId"] = new SelectList(
    _context.TrainingSessions
        .Select(ts => new
        {
            ts.Id,
            Display = ts.Name + " — " + ts.StartTime.ToString("dd.MM.yyyy HH:mm")
        }),
    "Id",
    "Display"
);

            ViewData["ExerciseTypeId"] = new SelectList(
    _context.ExerciseType,
    "Id",
    "Name"
);

            return View(model); 
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TrainingSessionId,ExerciseTypeId,Load,Sets,Repetitions")] ExercisePerformed exercisePerformed)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exercisePerformed);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrainingSessionId"] = new SelectList(
    _context.TrainingSessions
        .Select(ts => new
        {
            ts.Id,
            Display = ts.Name + " — " + ts.StartTime.ToString("dd.MM.yyyy HH:mm")
        }),
    "Id",
    "Display"
);

            ViewData["ExerciseTypeId"] = new SelectList(
    _context.ExerciseType,
    "Id",
    "Name"
);

            return View(exercisePerformed);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercisePerformed = await _context.ExercisePerformed.FindAsync(id);
            if (exercisePerformed == null)
            {
                return NotFound();
            }
            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseType, "Id", "Name", exercisePerformed.ExerciseTypeId);
            ViewData["TrainingSessionId"] = new SelectList(_context.TrainingSessions, "Id", "Name", exercisePerformed.TrainingSessionId);
            return View(exercisePerformed);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TrainingSessionId,ExerciseTypeId,Load,Sets,Repetitions")] ExercisePerformed exercisePerformed)
        {
            if (id != exercisePerformed.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exercisePerformed);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExercisePerformedExists(exercisePerformed.Id))
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
            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseType, "Id", "Name", exercisePerformed.ExerciseTypeId);
            ViewData["TrainingSessionId"] = new SelectList(_context.TrainingSessions, "Id", "Name", exercisePerformed.TrainingSessionId);
            return View(exercisePerformed);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercisePerformed = await _context.ExercisePerformed
                .Include(e => e.ExerciseType)
                .Include(e => e.TrainingSession)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exercisePerformed == null)
            {
                return NotFound();
            }

            return View(exercisePerformed);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exercisePerformed = await _context.ExercisePerformed.FindAsync(id);
            if (exercisePerformed != null)
            {
                _context.ExercisePerformed.Remove(exercisePerformed);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExercisePerformedExists(int id)
        {
            return _context.ExercisePerformed.Any(e => e.Id == id);
        }
    }
}
