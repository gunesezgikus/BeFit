using BeFit.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace BeFit.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                   : base(options)
        {

        }
        public DbSet<ExerciseType> ExerciseType { get; set; } = default!;
        public DbSet<TrainingSession> TrainingSessions { get; set; } = default!;
        public DbSet<Exercise> Exercises { get; set; } = default!;

    }
}
