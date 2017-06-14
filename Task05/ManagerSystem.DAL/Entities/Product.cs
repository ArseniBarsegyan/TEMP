using System;

namespace ManagerSystem.DAL.Entities
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public virtual Manager Manager { get; set; }
        public virtual Client Client { get; set; }
    }
}
