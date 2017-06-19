using System;

namespace UserStore.DAL.Entities
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public decimal Price { get; set; }
        public Manager Manager { get; set; }
    }
}
