using FreelancerPlatform.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace FreelancerPlatform.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Request>()
                .HasKey(r => new { r.FreelancerId, r.ProjectId });

            modelBuilder.Entity<Request>()
                .HasOne(r => r.Freelancer)
                .WithMany(u => u.Requests)
                .HasForeignKey(r => r.FreelancerId);

            modelBuilder.Entity<Request>()
                .HasOne(r => r.Project)
                .WithMany(p => p.Requests)
                .HasForeignKey(r => r.ProjectId);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.Freelancer)
                .WithMany(u => u.Projects)
                .HasForeignKey(p => p.FreelancerId);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Projects)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Models.Task>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Project)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.ProjectId);
        }
    }
}
