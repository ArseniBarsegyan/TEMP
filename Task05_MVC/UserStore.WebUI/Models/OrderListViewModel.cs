using System.Collections.Generic;
using System.Web.Mvc;
using UserStore.BLL.DTO;

namespace UserStore.WebUI.Models
{
    public class OrderListViewModel
    {
        public IEnumerable<OrderDto> Orders { get; set; }
        public SelectList Managers { get; set; }
        public SelectList Products { get; set; }
        public SelectList Dates { get; set; }
        public SelectList Prices { get; set; }
    }
}