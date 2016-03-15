using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Final.Models;
using System.Collections;
using Microsoft.AspNet.Identity;

namespace Final.Controllers
{
    public class ProductsController : Controller
    {
        private FinalContext db = new FinalContext();

        // GET: Products
        [Authorize(Roles ="Grower, Admin")]
        public ActionResult Index()
        {
            return View(GetMembersandProducts(0));
        }

        public ActionResult MemberIndex(string id)
        {
            return View(GetMemberProducts(id));
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductViewModel product = GetMemberandProduct(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,MemberID,Type,Strain,Quantity,Q_Type")] ProductViewModel productVM, string Types, string QuantityType)
        {
            if (ModelState.IsValid)
            {
                Member vendor = db.Users.Find(User.Identity.GetUserId());
                Member user = (from u in db.Users
                               where u.Id == vendor.Id
                               select u).FirstOrDefault();
                Product product = new Product()
                {

                    Type = Types,
                    Strain = productVM.Strain,
                    Quantity = productVM.Quantity,
                    Q_Type = QuantityType,
                    MemberName = user
                };
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("MemberIndex");
            }

            return View(productVM);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,MemberID,Type,Strain,Quantity,Q_Type")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MemberIndex");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
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
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("MemberIndex");
        }
        [Authorize(Roles ="Admin, Dispensary")]
        public ActionResult Search()
        {
            ProductViewModel dropdown = new ProductViewModel();
            dropdown.States = new List<State>();
            dropdown.States = GetStates();
            return View(dropdown);
        }
        [HttpPost]
        [Authorize(Roles = "Admin, Dispensary")]
        public ActionResult Search(int stateid)
        {
            //Get a list of message view models
            List<City> cities = new List<City>();
            cities = GetCities().Where(c => c.StateID == stateid).ToList();
            SelectList city = new SelectList(cities, "CityID", "CityName");
            return Json(cities);
        }
        [HttpPost]
        public ActionResult GetProducts(int? ddlcity)
        {
            var productVMs = new List<ProductViewModel>();
            var members = (from m in db.Users.Include("products")
                           where m.CityName.CityID == ddlcity
                           select m);
            db.Products.Load();
            foreach (Member m in members)
            {
                foreach (Product p in m.Products)
                {
                    productVMs.Add(new ProductViewModel()
                    {
                        Strain = p.Strain,
                        Type = p.Type,
                        Quantity = p.Quantity,
                        Q_Type = p.Q_Type,
                        MemberName = m,
                        ProductID = p.ProductID
                    });
                }
            }
           
            return View("IndexSearch", productVMs);
        }
        public List<State> GetStates()
        {
            var state = (from s in db.Cities
                         select s.StateName).Distinct().ToList();
            return state;
        }
        public List<City> GetCities()
        {
            var city = (from c in db.Users
                        where c.Category == "Grower"
                        select c.CityName).Distinct().ToList();

            return city;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private List<ProductViewModel> GetMembersandProducts(int? productID)
        {
            var product = new List<ProductViewModel>();
            //var categories = from category in db.Categories.Include("members")
            //select category;
            var members = from member in db.Users.Include("products")
                          select member;
            db.Products.Load();
            //var types = from type in db.Products.Include("type")
            //select type;
            //foreach (Category c in categories)
            //{
            foreach (Member m in members)
            {
                foreach (Product p in m.Products)
                {
                    if (p.ProductID == productID || 0 == productID)
                    {
                        var productVm = new ProductViewModel();
                        productVm.Strain = p.Strain;
                        productVm.Type = p.Type;
                        productVm.Quantity = p.Quantity;
                        productVm.Q_Type = p.Q_Type;
                        productVm.MemberName = m;

                        productVm.ProductID = p.ProductID;
                        product.Add(productVm);
                    }
                }
            }
            // }
            return product;
        }
        public ProductViewModel GetMemberandProduct(int? productID)
        {
            ProductViewModel productVM = (from p in db.Products
                                          join m in db.Users on p.MemberID equals m.MemberID
                                          where p.ProductID == productID
                                          select new ProductViewModel
                                          {
                                              ProductID = p.ProductID,
                                              Strain = p.Strain,
                                              Type = p.Type,
                                              Quantity = p.Quantity,
                                              Q_Type = p.Q_Type,
                                              MemberName = m
                                          }).FirstOrDefault();
            return productVM;
        }
        public List<ProductViewModel> GetMemberProducts(string id)
        {
            Member vendor = db.Users.Find(User.Identity.GetUserId());
            var productVMs = new List<ProductViewModel>();
            var member = (from m in db.Users.Include("Products")
                          where m.Id == vendor.Id
                          select m);
            db.Products.Load();
            foreach (Member m in member)
            {
                foreach (Product p in m.Products)
                {
                    productVMs.Add(new ProductViewModel()
                    {
                        Strain = p.Strain,
                        Type = p.Type,
                        Quantity = p.Quantity,
                        Q_Type = p.Q_Type,
                        MemberName = m,
                        ProductID = p.ProductID
                    });
                }
            }
            return productVMs;
        }
    }
}
