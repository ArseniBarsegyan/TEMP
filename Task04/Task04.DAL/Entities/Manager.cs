﻿using System;
using System.Collections.Generic;

namespace Task04.DAL.Entities
{
    public class Manager : Entity
    {
        public string LastName { get; set; }
        public DateTime Date { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
