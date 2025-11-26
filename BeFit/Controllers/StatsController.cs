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
    public class StatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var fourWeeksAgo = DateTime.Now.AddDays(-28);

            var stats = await _context.Exercises
                .Include(ep => ep.ExerciseType)
                .Include(ep => ep.TrainingSession)
                .Where(ep => ep.TrainingSession!.UserId == userId && ep.TrainingSession.StartTime >= fourWeeksAgo)
                .GroupBy(ep => ep.ExerciseType!.Name)
                .Select(g => new ExerciseStats
                {
                    ExerciseName = g.Key,
                    TotalSessions = g.Count(),
                    TotalReps = g.Sum(ep => ep.Sets * ep.Repetitions),
                    AvgLoad = g.Average(ep => ep.Load),
                    MaxLoad = g.Max(ep => ep.Load)
                })
                .ToListAsync();

            return View(stats);
        }
    }
}
