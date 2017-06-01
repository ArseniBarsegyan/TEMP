namespace Task04.DAL.Entities
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Client { get; set; }
        public decimal Price { get; set; }
        public Manager Manager { get; set; }
    }
}
