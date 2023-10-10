namespace LimisInsight.Models
{
    public class TimeEntry
    {
        public int Id { get; set; }
        public DateTime DateAt { get; set; }
        public float Duration { get; set; }
        public float DurationHour { get; set; }
        public int MembersUserId { get; set; }
        public string MembersUserName { get; set; }
    }
}
