namespace GameStore.Models
{
    public class Game : Entity
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}