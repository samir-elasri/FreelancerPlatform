using System.ComponentModel.DataAnnotations;

namespace FreelancerPlatform.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}
