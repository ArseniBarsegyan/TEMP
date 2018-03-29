using System;

namespace CTR.BLL.Dto
{
    public class ActivityDto
    {
        public int Id { get; set; }
        public ActivityType Type { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public string Description { get; set; }
    }

    public enum ActivityType
    {
        Walking,
        WatchingYoutube,
        Working,
        Learning,
        Reading,
        Training,
        Talking,
        InternetSerfing,
        Thinking
    }
}