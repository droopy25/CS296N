using CityWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CityWeb.DAL
{
    public class TopicsRepository : IDisposable, ITopicsRepository
    {
        CityWebContext db = new CityWebContext();
        public List<Topic> GetAllTopics()
        {
            return db.Topics.ToList();
        }

        public Topic GetTopicByID(int? id)
        {
            return db.Topics.Find(id);
        }

        public Topic AddTopic(Topic topic)
        {
            Topic dbTopic = db.Topics.Add(topic);
            db.SaveChanges();
            return dbTopic;
        }

        public int UpdateTopic(Topic topic)
        {
            db.Entry(topic).State = EntityState.Modified;
           return  db.SaveChanges();            
        }

        public Topic DeleteTopic(int id)
        {
            Topic topic = GetTopicByID(id);
            db.Topics.Remove(topic);
            db.SaveChanges();
            return topic;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}