using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityApi.Models
{
    [Table("Curso")]
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        [Column("Nome")]
        public string Name { get; set; }

        [StringLength(250)]
        [Column("Requisito")]
        public string? Requirement { get; set; }

        [Column("CargaHoraria")]
        public int? WorkLoad { get; set; }

        [Required]
        [Column("Valor")]
        public decimal? Value { get; set; }
    }
}
