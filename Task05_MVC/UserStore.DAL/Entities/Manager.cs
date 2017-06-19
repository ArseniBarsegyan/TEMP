using System.Collections.Generic;

namespace UserStore.DAL.Entities
{
    public class Manager : Entity
    {
        public Manager()
        {
            Products = new List<Product>();
        }

        public string LastName { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
