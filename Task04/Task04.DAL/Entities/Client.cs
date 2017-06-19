namespace Task04.DAL.Entities
{
    public class Client : Entity
    {
        public string Name { get; set; }
        public virtual Product Product { get; set; }
    }
}