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
        public DbSet<PublicHoliday> PublicHolidays { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                Id = 1,
                Name = "Mohsen Alizadeh",
                JobPosition = "Full Stack .NET Developer",
                Email = "M.Alizadeh.Net@outlook.com"
            });

            modelBuilder.Entity<PublicHoliday>().HasData(
                    new PublicHoliday { Id = 1, Date = new DateTime(2025, 1, 1), Description = "New Year’s Day" },
                    new PublicHoliday { Id = 2, Date = new DateTime(2025, 4, 18), Description = "Good Friday" },
                    new PublicHoliday { Id = 3, Date = new DateTime(2025, 4, 21), Description = "Easter Monday" },
                    new PublicHoliday { Id = 4, Date = new DateTime(2025, 5, 5), Description = "Early May Bank Holiday" },
                    new PublicHoliday { Id = 5, Date = new DateTime(2025, 5, 26), Description = "Spring Bank Holiday" },
                    new PublicHoliday { Id = 6, Date = new DateTime(2025, 8, 25), Description = "Summer Bank Holiday" },
                    new PublicHoliday { Id = 7, Date = new DateTime(2025, 12, 25), Description = "Christmas Day" },
                    new PublicHoliday { Id = 8, Date = new DateTime(2025, 12, 26), Description = "Boxing Day" }
                );
        }
    }
}