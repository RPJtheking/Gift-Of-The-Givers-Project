using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteClassLibrary
{
    public class User
    {
        [Key]
        public int User_ID { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [Required, MaxLength(100)]
        public string Email { get; set; }

        [Required, MaxLength(255)]
        public string PasswordHash { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(50)]
        public string Role { get; set; }

        // Relationships
        public ICollection<Donation> Donations { get; set; }
        public ICollection<Report> Reports { get; set; }
        public ICollection<Volunteer> Volunteers { get; set; }
    }
}
