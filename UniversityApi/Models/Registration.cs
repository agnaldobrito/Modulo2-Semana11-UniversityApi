using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityApi.Models
{
    [Table("Matricula")]
    public class Registration
    {
        public int Id { get; set; }

        [Required]
        [Column("IdTurma")]
        [ForeignKey("Class")]
        public int IdClass { get; set; }

        [Required]
        [Column("IdAluno")]
        [ForeignKey("Student")]
        public int IdStudent { get; set; }

        [Column("DataMatricula")]
        public DateTime? RegistrationDate { get; set; }

        public Class? Class { get; set; }
        public Student? Student { get; set; }


    }
}
