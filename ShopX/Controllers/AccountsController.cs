using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ShopX.Models;

namespace ShopX.Controllers
{
    public class AccountsController : Controller
    {
        private ShopEntities db = new ShopEntities();

        // GET: Accounts
        public ActionResult Index()
        {
            return View(db.Accounts.ToList());
        }

        // GET: Accounts/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account customer = db.Accounts.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Accounts/Create
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "CustomerID,UserName,Email,MobileNumber,Address,Password,ConformPassword")] Account customer)
        {
            if (ModelState.IsValid)
            {   
                TempData.Keep();
                customer.CustomerID = Guid.NewGuid();
                TempData["CurrentUserId"] = customer.CustomerID;
                TempData["CurrentUserName"] = customer.UserName;
                db.Accounts.Add(customer);
                db.SaveChanges();
                ViewBag.Message = "Registered Successfully";
                if (customer.Email == "admin@shop.com")
                {
                    return RedirectToAction("Create", "Products");
                }
                else
                {
                    return RedirectToAction("Home", "Products");
                }
            }
            return View(customer);

        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            Account customer = new Account();               
            return View(customer);
        }

       

        //
        // POST: /Account/Login
        [HttpPost]
        public ActionResult Login(Account customer)
        {
            Guid currentuserId=Guid.Empty;
            
            foreach (var item in db.Accounts)
            {
                if (item.Email == customer.Email)
                {                    
                    currentuserId =item.CustomerID;                    
                    break;
                }
            }

            if (currentuserId==Guid.Empty)
            {                 
                ViewBag.UserIdErrorMessage = "Username does not exist";
                return View(customer);
            }
            if (db.Accounts.Find(currentuserId).Password != customer.Password)
            {
                ViewBag.PasswordErrorMessage = "Incorrect Password";
                return View(customer);
            }
            else
            {
                TempData["CurrentUserId"] = currentuserId;
                TempData["CurrentUserName"] = db.Accounts.Find(currentuserId).UserName;
                if (customer.Email == "admin@shopx.com")
                {
                    return RedirectToAction("Create", "Products");
                }
                else
                {
                    return RedirectToAction("Home", "Products");
                }
            }

            


        }

        // GET: Accounts/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,UserName,Email,MobileNumber,Address,Password,ConformPassword")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: Accounts/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
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
        public ActionResult Admin()
        {
            TempData.Keep();
            return View();
        }
        public ActionResult Logout()
        {
            TempData.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}
