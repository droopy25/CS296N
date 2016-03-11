using System.Collections.Generic;
using CityWeb.Models;

namespace CityWeb.DAL
{
    public interface ITopicsRepository
    {
        Topic AddTopic(Topic topic);
        Topic DeleteTopic(int id);
        void Dispose();
        List<Topic> GetAllTopics();
        Topic GetTopicByID(int? id);
        int UpdateTopic(Topic topic);
    }
}