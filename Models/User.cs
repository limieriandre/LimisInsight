using System.ComponentModel.DataAnnotations.Schema;

namespace LimisInsight.Models
{
    [Table("organization_53257_teams_users_v2")]
    public class User
    {
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("user_name")]
        public string UserName { get; set; }

        [Column("team_id")]
        public int TeamId { get; set; }

        [Column("team_name")]
        public string TeamName { get; set; }
    }
}

