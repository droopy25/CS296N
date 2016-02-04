using System;
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

        // GET: Messages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MessageID,Category,Subject,Body,Date,From")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(message);
        }

        // GET: Messages/Edit/5
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
                return View("Details", messageVMs[0]);
            else 
                return View("Index", messageVMs);
            

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
