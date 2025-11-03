using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteClassLibrary
{
    public class Report
    {
        [Key]
        public int Report_ID { get; set; }

        [ForeignKey("ReliefProject")]
        public int ReliefProject_ID { get; set; }

        [ForeignKey("User")]
        public int User_ID { get; set; }


        public string Title { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        // Navigation
        public ReliefProject ReliefProject { get; set; }
        public User User { get; set; }
    }
}
