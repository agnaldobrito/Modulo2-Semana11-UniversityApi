using UniversityApi.Models;

namespace UniversityApi.Seeds
{
    public static class StudentSeed
    {
        public static List<Student> Seed { get; set; } = new List<Student>
        {
            new Student
            {
                Id = 1,
                Cpf = "123.321.121-50",
                Name = "student1",
                Email = "student1@email.com",
                Phone = "9876-5432",
                Birthday = new DateTime(1999,11,12)

            }
        };
    }
}
