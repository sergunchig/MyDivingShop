using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace MyDivingShop.Models
{
    public class Category
    {
        [Key]
        public int CatId { get; set; }
        public string CatName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }

    public class Company
    {
        [Key]
        public int ComId { get; set; }
        public string ComName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }

    public class Product
    {
        [Key]
        public int OrderCodeId { get; set; }
        public int CatId { get; set; }
        public int ComId { get; set; }
        public string ProdName { get; set; }
        public decimal Price { get; set; }
        public string UrlImage { get; set; }
        public int Quantity { get; set; }

        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }

    public class OrderItems
    {
        [Key]
        public int OrderItemsId { get; set; }
        public string OrderId { get; set; }
        public int OrderCodeId { get; set; }
        public int Quantity { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }

    public class Orders
    {
        [Key]
        public string OrderId { get; set; }
        public string ApplicationUserId { get; set; }
        public DateTime DateTime { get; set; }
        public int StatusId { get; set; }
        public string ShipCity { get; set; }
        public string ShipAddress { get; set; }
    }

    public class OrderStatus
    {
        [Key]
        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<Orders> orderStatus { get; set; }
    }
}