using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class ProductsController : Controller
    {
        private FinalProjectContext db = new FinalProjectContext();

        // GET: Products
        public ActionResult Index()
        {
            return View(GetMembersandProducts(0));
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
        public ActionResult Create([Bind(Include = "ProductID,MemberID,Type,Name,Quantity,Q_Type")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
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
        public ActionResult Edit([Bind(Include = "ProductID,MemberID,Type,Name,Quantity,Q_Type")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
            return RedirectToAction("Index");
        }
        public ActionResult Search()
        {

            var state = (from s in db.Members
                         select s.State).ToList();
            
            ViewBag.States = new SelectList(state.Distinct()); 
                        
            return View();
        }
        [HttpPost]
        public ActionResult Search(string state)
        {
            //Get a list of message view models
            //List<ProductViewModel> ProductVMs = new List<ProductViewModel>();

            //Get the subject that matches the search term
            //var city = (from c in db.Members
                          // where c.City.Contains(state)
                           //select c).ToList();
            //ViewBag.City = new SelectList(city.Distinct());
            return View("CitySearch", "Products");
            

        }
        public ActionResult CitySearch(string state)
        {
            var city = (from c in db.Members
                          where c.City.Contains(state)
                          select c).ToList();
            ViewBag.City = new SelectList(city.Distinct());        
            return View();
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
            var members = from member in db.Members.Include("products")
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
                                        join m in db.Members on p.MemberID equals m.MemberID
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
    }
}
