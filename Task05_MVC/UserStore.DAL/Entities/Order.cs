using System;

namespace UserStore.DAL.Entities
{
    public class Order : Entity
    {
        public DateTime Date { get; set; }
        public decimal OrderTotalPrice { get; set; }
        public int ManagerId { get; set; }
        public Manager Manager { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}