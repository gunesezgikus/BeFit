using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class ExerciseType
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Display(Name = "Exercise Name", Description = "Name of the exercise type")]
        public string Name { get; set; }
    }
}
