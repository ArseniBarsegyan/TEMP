using System;
using System.ComponentModel.DataAnnotations;
using CTR.BLL.Dto;

namespace CTR.MVC.Models
{
    public class ActivityViewModel
    {
        public int Id { get; set; }
        [Required]
        public ActivityType Type { get; set; }
        [Required]
        public TimeSpan Duration { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}