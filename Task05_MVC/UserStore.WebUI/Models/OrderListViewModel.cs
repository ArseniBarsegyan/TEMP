using System.Web.Mvc;
using PagedList;
using UserStore.BLL.DTO;

namespace UserStore.WebUI.Models
{
    public class OrderListViewModel
    {
        public IPagedList<OrderDto> Orders { get; set; }
        public SelectList Managers { get; set; }
        public SelectList Products { get; set; }
        public SelectList Dates { get; set; }
        public decimal FromValue { get; set; }
        public decimal ToValue { get; set; }
    }
}