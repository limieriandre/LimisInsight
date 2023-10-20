using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LimisInsight.Models
{
    [Table("data_justificada")]
    public class DataJustificada
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("OrganizationUser")]
        [Column("members_user_id")]
        public int MembersUserId { get; set; }

        [Column("data")]
        public DateTime Data { get; set; }

        [Required]
        [StringLength(255)]
        [Column("justificativa")]
        public string Justificativa { get; set; }

        // Relacionamento com a tabela organization_users
        public virtual OrganizationUser OrganizationUser { get; set; }
    }

}
