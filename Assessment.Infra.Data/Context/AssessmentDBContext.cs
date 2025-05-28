using Assessment.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Assessment.Infra.Data.Context
{
    public class AssessmentDBContext : DbContext
    {
        public AssessmentDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                Id = 1,
                Name = "Mohsen Alizadeh",
                Job = "Full Stack .NET Developer",
                Email = "M.Alizadeh.Net@outlook.com"
            });
        }
    }
}