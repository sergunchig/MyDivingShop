using MyDivingShop.Models;
using MyDivingShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyDivingShop2a.Models;
using MyDivingShop2a;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace MyDivingShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            if (user != null)
            {
                var rolle = userManager.GetRoles(user.Id);
                if (rolle.Count != 0)
                {
                    ViewBag.view = "visibility:visible";
                }
                else
                {
                    ViewBag.view = "visibility:hidden";
                }
            }
            else
            {
                ViewBag.view = "visibility:hidden";
            }
            var cart = ShoppingCart.GetCart(this.HttpContext);
            var viewModel = new ShopingCartViewModel()
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            return View(viewModel);
        }
        public ActionResult AddToCart(int id)
        {
            var addedProduct = context.products.Single(prod => prod.OrderCodeId == id);
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.AddToCart(addedProduct);

            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult Buy()
        {

            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);

            var cart = context.carts.ToList();
            var ordId = cart[0].CartId;
            context.orders.Add(new Orders
            {
                ApplicationUserId = user.Id,
                DateTime = DateTime.Now,
                StatusId = context.orderStatuses.Single(s => s.StatusName == "зарегестрирован").StatusId,
                ShipCity = context.Users.Find(user.Id).City,
                ShipAddress = context.Users.Find(user.Id).Address,
                OrderId =  ordId
            });

            foreach (var c in cart)
            {
                context.orderItems.Add(new OrderItems {
                    OrderCodeId = c.ProductId,
                    Quantity = c.QttProduct,
                    OrderId = ordId
                });
                context.carts.Remove(c);
            }

            ShoppingCart.sendMessage("Заказ принят");
            context.SaveChanges();
            return View();
        }
    }
}