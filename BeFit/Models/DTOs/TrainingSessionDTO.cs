using System.ComponentModel.DataAnnotations;

namespace BeFit.Models.DTOs
{
    public class TrainingSessionDTO
    {
        public int Id { get; set; }
        
        [Display(Name = "Antrenman Adı / Session Name")]
        public string Name { get; set; } = null!;

        [Display(Name = "Başlangıç Zamanı / Start Time")]
        public DateTime StartTime { get; set; }

        [Display(Name = "Bitiş Zamanı / End Time")]
        public DateTime EndTime { get; set; }
    }
}
