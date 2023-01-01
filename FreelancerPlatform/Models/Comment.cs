using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FreelancerPlatform.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10000)]
        public string Text { get; set; }

        [Required]
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
