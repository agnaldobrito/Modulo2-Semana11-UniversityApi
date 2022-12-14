using Microsoft.EntityFrameworkCore;
using UniversityApi.Models;

namespace UniversityApi.Context
{
    public class UniversityContext : DbContext
    {
        public UniversityContext() {}

        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options) {}

        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}
