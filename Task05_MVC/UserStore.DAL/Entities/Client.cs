using System.ComponentModel.DataAnnotations;

namespace UserStore.DAL.Entities
{
    public class Client : Entity
    {
        public string Name { get; set; }
        
        public int ProductId { get; set; }
        [Required]
        public Product Product { get; set; }
    }
}