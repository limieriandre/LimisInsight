using System.ComponentModel.DataAnnotations.Schema;

namespace LimisInsight.Models
{
    [Table("organization_53257_organization_users")]
    public class OrganizationUser
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("organization_user_state")]
        public string OrganizationUserState { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("role_name")]
        public string RoleName { get; set; }
    }
}
