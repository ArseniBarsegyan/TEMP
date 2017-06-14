using System.Collections.Generic;

namespace ManagerSystem.DAL.Entities
{
    public class Client
    {
        public Client()
        {
            Products = new List<Product>();
        }

        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}