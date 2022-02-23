using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopX.Models;

namespace ShopX.Controllers
{
    public class ProductsController : Controller
    {
        private ShopEntities db = new ShopEntities();

        
        public ActionResult Index()
        {
            TempData.Keep();
            return View(db.Products.ToList());
        }

        public ActionResult Details(Guid? id)
        {
            TempData.Keep();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult Create()
        {
            TempData.Keep();
            ProductViewModel productView = new ProductViewModel();
            return View(productView);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel productView)
        {
            TempData.Keep();
            if (ModelState.IsValid)                                         
            {                 
                string Image = Path.GetFileName(productView.ImagePath.FileName);
                productView.ImagePath.SaveAs(Server.MapPath("~/Images/" + Image));
                string Image_1 = Path.GetFileName(productView.ImagePath_1.FileName);
                productView.ImagePath_1.SaveAs(Server.MapPath("~/Images/" + Image_1));
                string Image_2 = Path.GetFileName(productView.ImagePath_2.FileName);
                productView.ImagePath_2.SaveAs(Server.MapPath("~/Images/" + Image_2));
                Product product = new Product
                {
                    ProductID = Guid.NewGuid(),                    
                    CategoryName = productView.CategoryName,
                    ProductName = productView.ProductName,
                    Price = productView.Price,
                    ImagePath = "~/Images/" + Image,
                    ImagePath1 = "~/Images/" + Image_1,
                    ImagePath2 = "~/Images/" + Image_2,
                    Description = productView.Description,
                    Specification = productView.Specification
                };
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Products/Edit/5
        public ActionResult Edit(Guid? id)
        {
            TempData.Keep();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,CategoryName,ProductName,Price,Description,ImagePath,ImagePath1,ImagePath2,Specification")] Product product)
        {
            TempData.Keep();
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(Guid? id)
        {
            TempData.Keep();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            TempData.Keep();
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Home()
        {
            TempData.Keep();
            return View();
        }

        public ActionResult IPhone()
        {
            TempData.Keep();
            return View(db.Products.ToList());
        }
        public ActionResult IPad()
        {
            TempData.Keep();
            return View(db.Products.ToList());
        }
        public ActionResult Watch()
        {
            TempData.Keep();
            return View(db.Products.ToList());
        }
        public ActionResult Mac()
        {
            TempData.Keep();
            return View(db.Products.ToList());
        }
        public ActionResult Accessories()
        {
            TempData.Keep();
            return View(db.Products.ToList());
        }
        public ActionResult AddToCart(Guid ProductID)
        {
            TempData.Keep();
            if (TempData["CurrentUserId"] == null)
            {
                TempData["LoginMessage"] = "Login to use this add to cart feature";
                return RedirectToAction("Login", "Accounts");
            }
            Cart cart = new Cart
            {
                CartID = Guid.NewGuid(),
                CustomerID = (Guid)TempData["CurrentUserId"],
                ProductID = ProductID
            };
            db.Carts.Add(cart);
            db.SaveChanges();
            return RedirectToAction("Bag","Carts");
        }
    }
       
}
                                