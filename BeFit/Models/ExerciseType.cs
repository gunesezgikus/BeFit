using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class ExerciseType
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Egzersiz adı gereklidir.")]
        [MaxLength(50, ErrorMessage = "Egzersiz adı en fazla 50 karakter olabilir.")]
        [Display(Name = "Egzersiz Adı / Exercise Name", Description = "Egzersiz türünün adı")]
        public string Name { get; set; } = null!;
    }
}
