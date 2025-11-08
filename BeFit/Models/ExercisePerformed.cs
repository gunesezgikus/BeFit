
using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class ExercisePerformed
    {
        public int Id { get; set; }
        [Display(Name = "Training Session", Description = "The session during which the exercise was performed")]
        public int TrainingSessionId { get; set; }

        [Display(Name = "Exercise Type", Description = "The type of exercise performed")]
        public int ExerciseTypeId { get; set; }

        [Display(Name = "Load (kg)", Description = "The weight used for the exercise")]
        public float Load { get; set; }

        [Display(Name = "Sets", Description = "Number of sets performed")]
        public int Sets { get; set; }

        [Display(Name = "Repetitions", Description = "Number of repetitions per set")]
        public int Repetitions { get; set; }

        public virtual TrainingSessions? TrainingSession { get; set; }
        public virtual ExerciseType? ExerciseType { get; set; }


    }
}
