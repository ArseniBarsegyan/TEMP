using System.ComponentModel.DataAnnotations;

namespace UserStore.DAL.Entities
{
    public class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}
