using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MyDivingShop2a.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace MyDivingShop.Models
{
    public class MyShopDbInitialize:DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new MyDivingShop2a.ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "user" };

            roleManager.Create(role1);
            roleManager.Create(role2);

            var admin = new ApplicationUser { Email = "sergunchig@yandex.ru", UserName = "sergunchig@yandex.ru", CustName = "admin" };
            string password = "P@ssw0rd";
            var result = userManager.Create(admin, password);

            if(result.Succeeded)
            {
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role2.Name);
            }

            var orderstat = new List<OrderStatus>
            {
                new OrderStatus{StatusName = "зарегестрирован"},
                new OrderStatus{StatusName = "в работе"},
                new OrderStatus{StatusName = "готов"},
                new OrderStatus{ StatusName = "доставлен"}
            };

            orderstat.ForEach(os => context.orderStatuses.Add(os));

            var categories = new List<Category> {new Category { CatName = "Маски", Description = "маска защищиае глаза и дыхателье пути ныряльщика от попадания в них воды" },
            new Category { CatName = "Ласты", Description = "" },
            new Category { CatName = "Трубки", Description = "" },
            new Category { CatName = "Мокрые идрокостюмы", Description = "" }
            };
            categories.ForEach(cat => context.categories.Add(cat));

            //var customers = new List<Customer> { new Customer { CustName = "Serjio", City = "Spb", Address = "Moskow hw 555" },
            //new Customer { CustName = "Alex", City = "Spb", Address = "Nevsky 800" }};
            //customers.ForEach(cust => context.customers.Add(cust));

            var companies = new List<Company>{new Company { ComName = "Cressi", Description = "Итальянский производитель оборудования для дайвинга" },
                new Company { ComName = "Omer", Description = "Итальянский производитель оборудования для дайвинга" },
                new Company { ComName = "Aqualung", Description = "" },
                new Company { ComName = "Tusa", Description = "" },
                new Company { ComName = "Orca", Description = "" },
                new Company { ComName = "Сарган", Description = "" },
                new Company { ComName = "Aquasphere", Description = "" },
            };
            companies.ForEach(com => context.companies.Add(com));

            var products = new List<Product>{new Product { CatId = 1 , ComId = 3 , ProdName = "Маска Aqualung-Technisub Reveal X2", Price = 3800, UrlImage = null, Quantity = 5 },
            new Product { CatId = 1 , ComId = 3 , ProdName = "Маска Aqualung Mission", Price = 2500, UrlImage = null, Quantity = 3 },
            new Product { CatId = 1 , ComId = 3 , ProdName = "Aqualung-Technisub Favola", Price = 2700, UrlImage = null, Quantity = 5 },
            new Product { CatId = 2 , ComId = 3 , ProdName = "Ласты Problue Tiara Pro F-770", Price = 3900, UrlImage = null, Quantity = 5 },
            new Product { CatId = 2 , ComId = 4 , ProdName = "Ласты Tusa imprex Duo", Price = 4900, UrlImage = null, Quantity = 5 },
            new Product { CatId = 2 , ComId = 2 , ProdName = "Ласты Apeks RK3", Price = 8800, UrlImage = null, Quantity = 5 },
            new Product { CatId = 3 , ComId = 7 , ProdName = "Трубка Сарган Сонора", Price = 1300, UrlImage = null, Quantity = 5 },
            new Product { CatId = 3 , ComId = 3 , ProdName = "Трубка Aqualung - Technisub Airflex Lx", Price = 1010, UrlImage = null, Quantity = 5 },
            new Product { CatId = 4 , ComId = 5 , ProdName = "Гидрокостюм Prodive Orca, лайкра", Price = 3500, UrlImage = null, Quantity = 5 },
            new Product { CatId = 4 , ComId = 5 , ProdName = "Гидрокостюм Aquasphere Stingray Shorty 2 mm ", Price = 5200, UrlImage = null, Quantity = 5 },
            };
            products.ForEach(pr => context.products.Add(pr));

            //context.orderItems.Add(new OrderItems { OrderCodeId = 1, OrderId = 1, Quantity = 2 });
            //context.orderItems.Add(new OrderItems { OrderCodeId = 1, OrderId = 1, Quantity = 1 });


            //context.orders.Add(new Orders { CustomerId = 1, DateTime = DateTime.Now, OrderStatus =1, ShipCity = "Spb", ShipAddress = "Moskow Hw 555"});

            base.Seed(context);
        }
    }
}