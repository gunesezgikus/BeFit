
using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class Exercise
    {
        public int Id { get; set; }

        [Display(Name = "Antrenman Oturumu / Training Session")]
        public int TrainingSessionId { get; set; }

        [Display(Name = "Egzersiz Türü / Exercise Type")]
        public int ExerciseTypeId { get; set; }

        [Range(0, 1000, ErrorMessage = "Weight must be between 0 and 1000 kg.")]
        [Display(Name = "Ağırlık (kg) / Load", Description = "Egzersiz için kullanılan ağırlık")]
        public float Load { get; set; }

        [Range(1, 100, ErrorMessage = "The number of sets should be between 1 and 100.")]
        [Display(Name = "Setler / Sets", Description = "Yapılan set sayısı")]
        public int Sets { get; set; }

        [Range(1, 1000, ErrorMessage = "The number of repetitions should be between 1 and 1000.")]
        [Display(Name = "Tekrarlar / Reps", Description = "Set başına tekrar sayısı")]
        public int Repetitions { get; set; }

        public virtual TrainingSession? TrainingSession { get; set; }
        public virtual ExerciseType? ExerciseType { get; set; }
    }
}
