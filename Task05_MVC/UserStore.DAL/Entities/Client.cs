using System.Collections.Generic;

namespace UserStore.DAL.Entities
{
    public class Client : Entity
    {
        public Client()
        {
            Products = new List<Product>();
        }

        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}