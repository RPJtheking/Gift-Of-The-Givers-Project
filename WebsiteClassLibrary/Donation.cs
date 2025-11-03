using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteClassLibrary
{
    public class Donation
    {
        [Key]
        public int Donation_ID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        public string Email { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string Status { get; set; }

        public User User { get; set; }
    }
}
