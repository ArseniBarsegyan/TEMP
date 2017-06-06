using System;

namespace TestMapper.DAO
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Client { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public Manager Manager { get; set; }
    }
}
