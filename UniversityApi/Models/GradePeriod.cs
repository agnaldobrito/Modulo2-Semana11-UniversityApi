using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityApi.Models
{
    [Table("NotaPeriodo")]
    public class GradePeriod
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Period { get; set; }
    }
}
