using System.ComponentModel.DataAnnotations.Schema;
using CTR.DAL.Util;

namespace CTR.DAL.Entities
{
    [Table(ConstantStore.Reports)]
    public class ReportEntity : Entity
    {
        public string Description { get; set; }
    }
}