using System.ComponentModel.DataAnnotations;

namespace BeFit.Models.DTOs
{
    public class TrainingSessionsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }
    }
}
