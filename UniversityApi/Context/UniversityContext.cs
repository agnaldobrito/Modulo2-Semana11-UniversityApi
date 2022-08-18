using Microsoft.EntityFrameworkCore;
using UniversityApi.Models;
using UniversityApi.Seeds;

namespace UniversityApi.Context
{
    public class UniversityContext : DbContext
    {
        public UniversityContext() {}

        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options) {}

        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<GradePeriod> GradePeriods { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Grade> Grades { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        modelBuilder.Entity<Student>().HasData(StudentSeed.Seed);
        }
    }

}
