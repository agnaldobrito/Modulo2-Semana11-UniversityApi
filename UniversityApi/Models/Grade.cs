using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityApi.Models
{
    [Table("Nota")]
    public class Grade
    {
        public int Id { get; set; }

        [Required]
        [Column("Nota")]
        public decimal Value { get; set; }

        [Required]
        [Column("IdMatricula")]
        [ForeignKey("Registration")]
        public int IdRegistration { get; set; }

        [Required]
        [Column("IdNotaPeriodo")]
        [ForeignKey("GradePeriod")]
        public int IdGradePeriod { get; set; }

        public Registration? Registration { get; set; }
        public GradePeriod? GradePeriod { get; set; }
    }
}
