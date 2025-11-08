using System.ComponentModel.DataAnnotations;

namespace BeFit.Models.DTOs
{
    public class ExercisePerformedDTO
    {
        public int Id { get; set; }

        [Display(Name = "Training Session")]
        public int TrainingSessionId { get; set; }

        [Display(Name = "Exercise Type")]
        public int ExerciseTypeId { get; set; }

        [Display(Name = "Load (kg)")]
        public float Load { get; set; }

        [Display(Name = "Sets")]
        public int Sets { get; set; }

        [Display(Name = "Repetitions")]
        public int Repetitions { get; set; }

       
        public string? ExerciseTypeName { get; set; }
        public string? TrainingSessionName { get; set; }
    }
}
