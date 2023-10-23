using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LimisInsight.Models
{
    [Table("data_feriado")]
    public class DataFeriado
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("data", TypeName = "Date")]
        public DateTime Data { get; set; }

        [Required]
        [StringLength(500)] // Assumindo que a justificativa pode ter até 500 caracteres, ajuste conforme necessário
        [Column("justificativa")]
        public string Justificativa { get; set; }
    }
}
