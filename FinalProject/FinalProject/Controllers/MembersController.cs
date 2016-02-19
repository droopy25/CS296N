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
    public class MembersController : Controller
    {
        private FinalProjectContext db = new FinalProjectContext();

        // GET: Members
        public ActionResult Index()
        {
            return View(GetCategorysandMemberandProduct(0));
        }

        // GET: Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberViewModel member = GetMemberandProduct(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberID,CategoryID,Name,Street,City,State,Phone,Email")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(member);
        }

        // GET: Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberViewModel member = GetMemberandProduct(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberID,CategoryID,Name,Street,City,State,Phone,Email")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member);
        }

        // GET: Members/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
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
        private List<MemberViewModel> GetCategorysandMemberandProduct(int? memberID)
        {
            var memberM = new List<MemberViewModel>();
            var categories = from category in db.Categories.Include("members")
                         select category;
            var members = from member in db.Members.Include("products")
                          select member;
            db.Products.Load();
            //var types = from type in db.Products.Include("type")
                        //select type;
            foreach (Category c in categories)
            {
                foreach (Member m in c.Members)
                {
                    foreach (Product p in m.Products)
                    {
                        if (p.MemberID == memberID || 0 == memberID)
                        {
                            var memberVm = new MemberViewModel();
                            memberVm.Name = m.Name;
                            memberVm.Street = m.Street;
                            memberVm.City = m.City;
                            memberVm.State = m.State;
                            memberVm.Phone = m.Phone;
                            memberVm.Email = m.Email;
                            memberVm.CategoryName = c;
                            memberVm.ProductName = p;
                            memberVm.MemberID = m.MemberID;
                            memberM.Add(memberVm);
                        }
                    }
                }
            }
            return memberM;
        }
        public MemberViewModel GetMemberandProduct(int? memberID)
        {
            MemberViewModel memberVM = (from m in db.Members
                                        join p in db.Products on m.ProductID equals p.ProductID
                                        where m.MemberID == memberID
                                        select new MemberViewModel
                                        {
                                            MemberID = m.MemberID,
                                            Name = m.Name,
                                            //Body = m.Body,
                                            //Date = m.Date,
                                            //From = m.From,
                                            ProductName = p
                                        }).FirstOrDefault();
            return memberVM;
        }
    }
}
