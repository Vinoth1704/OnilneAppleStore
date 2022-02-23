using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopX.Models;

namespace ShopX.Controllers
{
    public class CartsController : Controller
    {
        private ShopEntities db = new ShopEntities();
        public ActionResult Bag()
        {
            if (TempData["CurrentUserId"] == null)
            {
                TempData["LoginMessage"] = "Login to use this cart feature";
                return RedirectToAction("Login", "Accounts");
            }
            else
            {
                TempData.Keep();
                return View(db.Carts.ToList()); ;
            }
        }
        public ActionResult RemoveFromCart(Guid ProductID)
        {
            TempData.Keep();
            foreach (var cartItem in db.Carts)
                if (cartItem.ProductID == ProductID)
                {
                    db.Carts.Remove(cartItem);
                    break;
                }             
            db.SaveChanges();
            return RedirectToAction("Bag", "Carts");
        }
    }
}
