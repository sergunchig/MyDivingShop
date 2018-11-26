using MyDivingShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDivingShop2a.ViewModels
{
    public class OrdersViewModel
    {
        public List<Product> products { get; set; }
        public List<OrderItems> orderItems { get; set; }
        public string Name { get; set; }
    }
}