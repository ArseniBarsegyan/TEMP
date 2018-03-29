using System;
using System.ComponentModel.DataAnnotations.Schema;
using CTR.DAL.Util;

namespace CTR.DAL.Entities
{
    [Table(ConstantStore.Activities)]
    public class ActivityEntity : Entity
    {
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public string Description { get; set; }
    }
}