using System;

namespace CTR.BLL.Dto
{
    public class ActivityUpdateDto
    {
        public int Id { get; set; }
        public ActivityType Type { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public string Description { get; set; }
    }
}