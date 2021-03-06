﻿using System;

namespace Task04.DAL.Entities
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public virtual Client Client { get; set; }
        public decimal Price { get; set; }
        public virtual Manager Manager { get; set; }
    }
}
