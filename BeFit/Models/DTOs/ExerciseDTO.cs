using System.ComponentModel.DataAnnotations;

namespace BeFit.Models.DTOs
{
    public class ExerciseDTO
    {
        public int Id { get; set; }

        [Display(Name = "Antrenman Oturumu / Training Session")]
        public int TrainingSessionId { get; set; }

        [Display(Name = "Egzersiz Türü / Exercise Type")]
        public int ExerciseTypeId { get; set; }

        [Display(Name = "Ağırlık (kg) / Load")]
        public float Load { get; set; }

        [Display(Name = "Setler / Sets")]
        public int Sets { get; set; }

        [Display(Name = "Tekrarlar / Reps")]
        public int Repetitions { get; set; }

       
        public string? ExerciseTypeName { get; set; }
        public string? TrainingSessionName { get; set; }
    }
}
