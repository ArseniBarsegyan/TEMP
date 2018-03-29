using System;
using System.ComponentModel.DataAnnotations;

namespace CTR.DAL.Entities
{
    public class Entity
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
    }
}