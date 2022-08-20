using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityApi.Models
{
    [Table("Turma")]
    public class Class
    {
        public int Id { get; set; }

        [Required]
        [Column("IdInstrutor")]
        [ForeignKey("Instructor")]
        public int IdInstructor { get; set; }

        [Required]
        [Column("IdCurso")]
        [ForeignKey("Course")]
        public int IdCourse { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }

        [Column("CargaHoraria")]
        public int? WorkLoad { get; set; }

        public Instructor? Instructor { get; set; }
        public Course? Course { get; set; }

    }
}
