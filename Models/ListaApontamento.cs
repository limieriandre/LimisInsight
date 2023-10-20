using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LimisInsight.Models
{
    [Table("lista_apontamentos")]
    public class ListaApontamento
    {
        [Column("ID_USER")]
        public int IdUser { get; set; }

        [Column("NOME")]
        public string Nome { get; set; }

        [Column("DATA")]
        public DateTime Data { get; set; }

        [Column("TIME")]
        public string Time { get; set; }

        [Column("SOMA")]
        public float Soma { get; set; }

        [Column("STATUS")]
        public string Status { get; set; }
    }
}
