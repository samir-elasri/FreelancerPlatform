using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FreelancerPlatform.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(10000)]
        public string Description { get; set; }

        [Required]
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
