﻿using System.Collections.Generic;

namespace ManagerSystem.DAL.Entities
{
    public class Manager : Entity
    {
        public Manager()
        {
            Products = new List<Product>();
        }

        public string LastName { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
