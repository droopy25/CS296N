using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CityWeb.Models;

namespace CityWeb.DAL
{
    public class FakeTopicRepository : ITopicsRepository
    {
        private List<Topic> topics = new List<Topic>();
        private int maxId = 0;

        public FakeTopicRepository()
        {
            topics = new List<Topic>();
        }

        public FakeTopicRepository(List<Topic> t)
        {
            topics = t;
        }

        public Topic AddTopic(Topic topic)
        {
            topic.TopicID = ++maxId;
            topics.Add(topic);
            return topic;
        }

        public Topic DeleteTopic(int id)
        {
            Topic topic = GetTopicByID(id);
            topics.Remove(topic);
            return topic;         
        }

        public void Dispose()
        {
            //Nothing to do
        }

        public List<Topic> GetAllTopics()
        {
            return topics;
        }

        public Topic GetTopicByID(int? id)
        {
            return topics.Find(t => t.TopicID == id);
        }

        public int UpdateTopic(Topic topic)
        {
            int topicUpdated = 0;
            if (DeleteTopic(topic.TopicID) != null)
            {
                topics.Add(topic);
                topicUpdated = 1;
            }
            

            return topicUpdated;
        }
    }
}