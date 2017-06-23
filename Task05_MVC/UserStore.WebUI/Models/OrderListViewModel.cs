﻿using System.Collections.Generic;
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
        public decimal FromValue { get; set; }
        public decimal ToValue { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}