using Microsoft.EntityFrameworkCore;
using ApiEfProject.Models;

namespace ApiEfProject.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectType> ProjectTypes { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One to many for ProjectType to Projects
            modelBuilder.Entity<ProjectType>()
                .HasMany(pt => pt.Projects)
                .WithOne(p => p.ProjectType)
                .HasForeignKey(p => p.ProjectTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            // One to many for Team to Projects
            modelBuilder.Entity<Team>()
                .HasMany(t => t.Projects)
                .WithOne(p => p.Team)
                .HasForeignKey(p => p.TeamId)
                .OnDelete(DeleteBehavior.NoAction);

            // One to many for Role to Developers
            modelBuilder.Entity<Role>()
                .HasMany(r => r.Developers)
                .WithOne(d => d.Role)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.NoAction);

            // One to many for Team to Developers
            modelBuilder.Entity<Team>()
                .HasMany(t => t.Developers)
                .WithOne(d => d.Team)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}