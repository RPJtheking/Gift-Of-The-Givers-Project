using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteClassLibrary
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
