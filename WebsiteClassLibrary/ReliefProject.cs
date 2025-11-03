using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteClassLibrary
{
    public class ReliefProject
    {
        [Key]
        public int ReliefProject_ID { get; set; }

        public string ProjectName { get; set; }

        public string Status { get; set; }

        public string Location { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Navigation
        public ICollection<Report> Reports { get; set; }
        public ICollection<Volunteer> Volunteers { get; set; }
        public ICollection<Resource> Resources { get; set; }
    }
}
