using System;
using KnowledgeBaseEmployees.Models;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeBaseEmployees.Data
{
    public class KnowledgeDBContext : DbContext
    {
        public KnowledgeDBContext(DbContextOptions<KnowledgeDBContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectEmployee> ProjectEmployees { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<TechnologyEmployee> TechnologyEmployees { get; set; }
        public DbSet<TechnologyProject> TechnolgyProjects { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().ToTable("Project");
            modelBuilder.Entity<ProjectEmployee>().ToTable("ProjectEmployee");
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Technology>().ToTable("Technology");
            modelBuilder.Entity<TechnologyEmployee>().ToTable("TechnologyEmployee");
            modelBuilder.Entity<TechnologyProject>().ToTable("TechnologyProject");
            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}
