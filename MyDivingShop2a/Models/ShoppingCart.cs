using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using MyDivingShop2a.Models;

namespace MyDivingShop.Models
{
    public class ShoppingCart
    {
        ApplicationDbContext context = new ApplicationDbContext();

        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";
        public List<Cart> GetCartItems()
        {
            return context.carts.Where(c => c.CartId == ShoppingCartId).ToList();
        }

        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        public void AddToCart(Product product)
        {
            var cartItem = context.carts.SingleOrDefault(c => c.CartId == ShoppingCartId && c.ProductId == product.OrderCodeId);
            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    ProductId = product.OrderCodeId,
                    ProductName = product.ProdName,
                    ProductPrice = product.Price,
                    CartId = ShoppingCartId,
                    QttProduct = 1,
                    dateTime = DateTime.Now
                };
                context.carts.Add(cartItem);
            }
            else
            {
                cartItem.QttProduct++;
            }
            context.SaveChanges();
        }

        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    Guid tempCartId = new Guid();
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }
        public decimal GetTotal()
        {
            try
            {
                decimal? total = context.carts.Where(cart => cart.CartId == ShoppingCartId).Select(cart => cart.QttProduct * cart.ProductPrice).Sum();
                return total ?? decimal.Zero;
            }
            catch(Exception e)
            {
                return decimal.Zero;
            }
        }
        public int GetQuantity()
        {
            int? count = context.carts.Where(cart => cart.CartId == ShoppingCartId).Select(cart => (int?)cart.QttProduct).Sum();
            return count ?? 0;
        }
        public int RemoveFromCart(int id)
        {
            var cartItem = context.carts.Single(cart => cart.CartId == ShoppingCartId && cart.ProductId == id);
            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.QttProduct > 1)
                {
                    cartItem.QttProduct--;
                    itemCount = cartItem.QttProduct;
                }
                else
                {
                    context.carts.Remove(cartItem);
                }
                context.SaveChanges();
            }
            return itemCount;
        }

        public static void sendMessage(string message)
        {
            MailAddress from = new MailAddress("sergunchig@gmail.com", "Sergey b");
            MailAddress to = new MailAddress("sergunchig@yandex.ru");
            MailMessage mailMessage = new MailMessage(from, to);
            mailMessage.Subject = message;
            mailMessage.Body = message;
            mailMessage.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("sergunchig@gmail.com", "bigpi3dec");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(mailMessage);
            mailMessage.Dispose();
        }
    }
}