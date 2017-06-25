﻿using System;
using System.ComponentModel.DataAnnotations;

namespace UserStore.WebUI.Models
{
    public class OrderCreateModel
    {
        [Required]
        public string ClientName { get; set; }

        [Required]
        public string ManagerName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The length of this line must be from 2 to 50 symbols")]
        public string ProductName { get; set; }

        [Required]
        public decimal Price { get; set; }
        
        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
    }
}