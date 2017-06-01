using System;
using Task04.DAL.Entities;

namespace Task04_BL.Models
{
    public class Purchase
    {
        public Purchase(Manager manager, Product product, string Client)
        {
            Manager = manager;
            Date = DateTime.Now;
        }

        public DateTime Date { get; set; }
        public Manager Manager { get; set; }
        public Product Product { get; set; }
        public string Client { get; set; }
    }
}
