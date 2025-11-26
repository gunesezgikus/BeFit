using System.ComponentModel.DataAnnotations;

namespace BeFit.Models.DTOs
{
    public class ExerciseTypeDTO
    {
        public int Id { get; set; }

        [Display(Name = "Egzersiz Adı / Exercise Name")]
        public string Name { get; set; } = null!;
    }
}
