using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class TrainingSessions
    {
        public int Id { get; set; }
        [Display(Name = "Session Name", Description = "Name of the training session")]
        public string Name { get; set; }
        [Display(Name = "Start Time", Description = "Start date and time of the session")]
        public DateTime StartTime { get; set; }
        [Display(Name = "End Time", Description = "End date and time of the session")]
        public DateTime EndTime { get; set; }
    }
}
