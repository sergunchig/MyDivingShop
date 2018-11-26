using MyDivingShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyDivingShop2a.Models;
using MyDivingShop2a.ViewModels;

namespace MyDivingShop.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {

        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Admin()
        {
            return View();
        }
        public ActionResult CreateCategory()
        {
            return View();
        }
        public ActionResult Create()
        {
            ViewBag.CatId = new SelectList(context.categories, "CatId", "CatName");
            ViewBag.ComId = new SelectList(context.companies, "ComId", "ComName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if(ModelState.IsValid)
            {
                context.products.Add(product);
                context.SaveChanges();
            }
            return View("Admin");
        }
        [HttpPost]
        public ActionResult CreateCategory(Category cat)
        {
            if (ModelState.IsValid)
            {
                context.categories.Add(cat);
                context.SaveChanges();
            }
            return View("Admin");
        }


        public ActionResult Orders()
        {
            IEnumerable<Orders> orders = context.orders;
            return View(orders);
        }


        public ActionResult Info(string id)
        {
            var prodList = context.products.ToList();
            var orderItems = context.orderItems.Where(oi => oi.OrderId == id).ToList();
            var nameId = context.orders.Where(o => o.OrderId == id).ToList();
            var nameIdd = nameId[0].ApplicationUserId;
            var name = context.Users.Where(n => n.Id == nameIdd).ToList();
            var orderModel = new OrdersViewModel { products = prodList, orderItems = orderItems, Name = name[0].CustName };
            return View(orderModel);
        }
    }
}