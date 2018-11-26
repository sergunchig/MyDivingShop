using MyDivingShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyDivingShop2a.Models;
using Microsoft.AspNet.Identity.Owin;
using MyDivingShop2a;
using Microsoft.AspNet.Identity;

namespace MyDivingShop.Controllers
{
    public class HomeController : Controller
    {        
        ApplicationDbContext db = new ApplicationDbContext();

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
                
            IEnumerable<Product> products = db.products;
            ViewBag.produkts = products;
            var cat = db.categories.ToList();
            return View(cat);
        }

        public ActionResult Browse(int cat)
        {
            var catModel = db.products.Where(cm=> cm.CatId == cat);
            return View(catModel);
        }

        [ChildActionOnly]
        public ActionResult categoriesMenu()
        {
            var cat = db.categories.ToList();
            return PartialView(cat);
        }

    }
}