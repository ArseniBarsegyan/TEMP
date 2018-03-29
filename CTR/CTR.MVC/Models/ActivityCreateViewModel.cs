using System;
using System.ComponentModel.DataAnnotations;
using CTR.BLL.Dto;

namespace CTR.MVC.Models
{
    public class ActivityCreateViewModel
    {
        [Required]
        public ActivityType Type { get; set; }
        [Required]
        public TimeSpan Duration { get; set; }
        [Required]
        public string Description { get; set; }
    }
}