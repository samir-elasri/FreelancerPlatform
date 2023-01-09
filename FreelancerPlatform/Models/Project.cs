using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace FreelancerPlatform.Models
{
    public class Project
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(10000)]
        public string Description { get; set; }

        [DefaultValue(true)]
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("Freelancer")]
        public int? FreelancerId { get; set; }
        public User? Freelancer { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Task> Tasks { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Request>? Requests { get; set; }

        public Project()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
