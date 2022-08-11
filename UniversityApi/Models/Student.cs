using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityApi.Models
{
    [Table("Aluno")]
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [StringLength(18)]
        public string Cpf { get; set; }

        [Required]
        [StringLength(150)]
        [Column("Nome")]
        public string Name { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(14)]
        [Column("Telefone")]
        public string Phone { get; set; }

        [Column("DataNascimento")]
        public DateTime? Birthday { get; set; }
    }
}
