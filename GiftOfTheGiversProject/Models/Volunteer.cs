using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGiversProject.Models
{
    public class Volunteer
    {
        [Key]
        public int Volunteer_ID { get; set; }

        [ForeignKey("User")]
        public int User_ID { get; set; }

        public string Username { get; set; }

        [ForeignKey("ReliefProject")]
        public int Project_ID { get; set; }

        public string Availability { get; set; }

        // Navigation
        public User User { get; set; }
        public ReliefProject ReliefProject { get; set; }

    }
}
