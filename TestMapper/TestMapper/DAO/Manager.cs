using System.Collections.Generic;

namespace TestMapper.DAO
{
    public class Manager : Entity
    {
        public Manager()
        {
            Products = new List<Product>();
        }

        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
