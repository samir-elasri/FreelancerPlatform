using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FreelancerPlatform.Models
{
    public class Request
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Freelancer")]
        public int FreelancerId { get; set; }
        public User Freelancer { get; set; }

        [Required]
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        [Required]
        public bool Approved { get; set; } = false;

        
    }
}
