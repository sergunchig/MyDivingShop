using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyDivingShop.Models
{
    public class Cart
    {
        [Key]
        public long NN { get; set; }
        public string CartId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public DateTime dateTime { get; set; }
        public int QttProduct { get; set; }

    }
}