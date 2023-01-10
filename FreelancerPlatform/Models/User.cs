using System.ComponentModel.DataAnnotations;

namespace FreelancerPlatform.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        public string UserType { get; set; } = "freelancer";

        public ICollection<Project>? Projects { get; set; }
        public ICollection<Request>? Requests { get; set; }

        
    }
}
