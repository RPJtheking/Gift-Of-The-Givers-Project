using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGiversProject.Models
{
    public class Resource
    {
        [Key]
        public int Resource_ID { get; set; }

        [ForeignKey("ReliefProject")]
        public int Project_ID { get; set; }

        [MaxLength(100)]
        public string ResourceType { get; set; }

        public int Quantity { get; set; }

        [MaxLength(50)]
        public string Status { get; set; }

        // Navigation
        public ReliefProject ReliefProject { get; set; }

    }
}
