﻿using System;

namespace TestMapper.DTO
{
    public class PurchaseDto
    {
        public string ManagerName { get; set; }
        public string ClientName { get; set; }
        public string ProductName { get; set; }
        public DateTime Date { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
