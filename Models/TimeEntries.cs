using System.ComponentModel.DataAnnotations.Schema;


namespace LimisInsight.Models
{
    [Table("organization_53257_time_entries_v2")]
    public class TimeEntries

    {
        [Column("id")]
        public int Id { get; set; }

        [Column("date_at")]
        public DateTime DateAt { get; set; }

        [Column("duration")]
        public float Duration { get; set; }

        [Column("duration_hour")]
        public float DurationHour { get; set; }

        [Column("members_user_id")]
        public int UserId { get; set; }

        [Column("members_user_name")]
        public string UserName { get; set; }
    }
}
