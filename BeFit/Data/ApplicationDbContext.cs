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
        public DbSet<TrainingSessions> TrainingSessions { get; set; } = default!;
        public DbSet<ExercisePerformed> ExercisePerformed { get; set; } = default!;

    }
}
