using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class TrainingSession
    {
        public int Id { get; set; }

        [Display(Name = "Antrenman Adı / Session Name", Description = "Antrenman oturumunun adı")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Başlangıç zamanı gereklidir.")]
        [Display(Name = "Başlangıç Zamanı / Start Time", Description = "Oturumun başlangıç tarihi ve saati")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "Bitiş zamanı gereklidir.")]
        [Display(Name = "Bitiş Zamanı / End Time", Description = "Oturumun bitiş tarihi ve saati")]
        public DateTime EndTime { get; set; }

        public string? UserId { get; set; }
    }
}
