using System;
using System.Collections.Generic;

namespace ManagerService.Models
{
    public class ManagerDto
    {
        public string LastName { get; set; }
        public DateTime Date { get; set; }

        public ICollection<ProductDto> Products { get; set; }
    }
}
