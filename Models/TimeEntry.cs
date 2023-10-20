using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LimisInsight.Models
{
    [Table("organization_53257_time_entries_v2")]
    public class TimeEntry
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("duration_hour")]
        public float DurationHour { get; set; }

        [Column("members_user_name")]
        public string MembersUserName { get; set; }

        [Column("activity_id")]
        public int ActivityId { get; set; }

        [Column("activity_title")]
        public string ActivityTitle { get; set; }

        [Column("time_entry_created_at")]
        public DateTime TimeEntryCreatedAt { get; set; }

        [Column("time_entry_updated_at")]
        public DateTime TimeEntryUpdatedAt { get; set; }


        [Column("date_at")]
        public DateTime DateAt { get; set; }

        [Column("members_user_id")]
        public int MembersUserId { get; set; }

        [Column("activity_status_name")]
        public string ActivityStatusName { get; set; }
    }
}
