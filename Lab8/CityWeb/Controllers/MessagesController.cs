﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CityWeb.Models;

namespace CityWeb.Controllers
{
    public class MessagesController : Controller
    {
        private CityWebContext db = new CityWebContext();

        // GET: Messages
        public ActionResult Index()
        {
            return View(GetTopicsandMessages(0)); 
        }

        // GET: Messages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageViewModel message = GetTopicandMessage(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }
        public ActionResult DetailsSearch(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageViewModel message = GetTopicandMessage(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // GET: Messages/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Topics =
                new SelectList(db.Topics.OrderBy(s => s.Category), "TopicID", "Category");
            ViewBag.Users =
                new SelectList(db.Users.Distinct().OrderBy(m => m.From), "UserName", "From");
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "MessageID,Subject,Body,Date,From,TopicName,Topics,Users")] MessageViewModel messageVM, int Topics, int Users)
        {
            if (ModelState.IsValid)
            {
                Topic topic = (from s in db.Topics
                               where s.TopicID == Topics
                               select s).FirstOrDefault();

                Message user = (from u in db.Messages
                                where u.MessageID == Users
                                select u).FirstOrDefault();

                

                if (topic == null)
                {
                    topic = new Topic() { Category = messageVM.TopicName.Category };
                    db.Topics.Add(topic);
                }
                if (user == null)
                {
                    user = new Message() { From = messageVM.From };
                    db.Messages.Add(user);
                }

                Message message = new Message()
                {
                    Subject = messageVM.Subject,
                    MessageID = messageVM.MessageID,
                    Body = messageVM.Body,
                    Date = messageVM.Date,
                    TopicID = topic.TopicID,
                    From = user.From                                    
                };
                db.Messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(messageVM);
        }

        // GET: Messages/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "MessageID,Category,Subject,Body,Date,From")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(message);
        }

        // GET: Messages/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string searchTerm)
        {
            //Get a list of message view models
            List<MessageViewModel> messageVMs = new List<MessageViewModel>();

            //Get the subject that matches the search term
            var message = (from m in db.Messages
                                    where m.Subject.Contains(searchTerm)
                                    select m).ToList<Message>();
            //In a loop:            
            //Create view models for the subject and put then in the list of view models
            foreach(Message m in message)
            {
                //TODO: Get the topic that contains each message
                var topic = (from t in db.Topics
                             where t.TopicID == m.TopicID
                             select t).FirstOrDefault();
                //Get the topic that contains the subject
                messageVMs.Add(new MessageViewModel() { Subject = m.Subject,
                                                        TopicName = topic,
                                                        Body = m.Body,
                                                        Date = m.Date,
                                                        From = m.From,
                                                        MessageID = m.MessageID});
            }

            /*List<Message> message = from m in db.Messages
                                    join t in db.Topics on m.TopicID equals t.TopicID
                                    where m.Subject.Contains(searchTerm)
                                    select new List<MessageViewModel>
                                    {
                                        MessageID = m.MessageID,
                                        Subject = m.Subject,
                                        Body = m.Body,
                                        Date = m.Date,
                                        From = m.From,
                                        TopicName = t
                                    };*/
            //If there is just one book display the detail view 
            if (messageVMs.Count == 1)
                return View("DetailsSearch", messageVMs[0]);
            else 
                return View("IndexSearch", messageVMs);
            

            //If there is multiple books display the index view

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private List<MessageViewModel> GetTopicsandMessages (int? messageID)
        {
            var messages = new List<MessageViewModel>();
            var topics = from topic in db.Topics.Include("messages")
                         select topic;
            foreach (Topic t in topics)
            {
                foreach (Message m in t.Messages)
                {
                    if (m.MessageID == messageID || 0 == messageID)
                    {
                        var messageVm = new MessageViewModel();
                        messageVm.Subject = m.Subject;
                        messageVm.Date = m.Date;
                        //messageVm.Body = m.Body;
                        messageVm.From = m.From;
                        messageVm.TopicName = t;
                        messageVm.MessageID = m.MessageID;
                        messages.Add(messageVm);
                    }
                }
            }
            return messages;
        }
        private MessageViewModel GetTopicandMessage(int? messageID)
        {
            MessageViewModel messageVM = (from m in db.Messages
                                          join t in db.Topics on m.TopicID equals t.TopicID
                                          where m.MessageID == messageID
                                          select new MessageViewModel
                                          {
                                              MessageID = m.MessageID,
                                              Subject = m.Subject,
                                              Body = m.Body,
                                              Date = m.Date,
                                              From = m.From,
                                              TopicName = t
                                          }).FirstOrDefault();
            return messageVM;
        }
    }
}
