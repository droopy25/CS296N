using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CityWeb.DAL;
using CityWeb.Models;
using System.Collections.Generic;
using CityWeb.Controllers;
using System.Web.Mvc;

namespace TopicsControllerTests
{
    [TestClass]
    public class ControllerTests
    {
        [TestMethod]
        public void IndexTest()
        {
            //arrange
            var topics = new List<Topic>()
            {
                new Topic() {TopicID = 1, Category = "Jobs" },
                new Topic() {TopicID = 2, Category = "General" },
                new Topic() {TopicID = 3, Category = "Meetings" }
            };

            var repo = new FakeTopicRepository();
            var target = new TopicsController(repo);
            //act
            var result = (ViewResult)target.Index();
            //assert
            var model = (List<Topic>)result.Model;
            Assert.AreEqual(model[0].TopicID, 1);
            Assert.AreEqual(model[1].TopicID, 2);
            Assert.AreEqual(model[2].TopicID, 3);
            Assert.AreEqual(model.Count, 3);
            

        }
        [TestMethod]
        public void DetailsTest()
        {
            //arrange
            var topics = new List<Topic>()
            {
                new Topic() {TopicID = 1, Category = "Jobs" },
                new Topic() {TopicID = 2, Category = "General" },
                new Topic() {TopicID = 3, Category = "Meetings" }
            };

            var repo = new FakeTopicRepository(topics);
            var target = new TopicsController(repo);
            //act
            var result = (ViewResult)target.Details(2);
            //assert
            var model = (Topic)result.Model;
            Assert.AreEqual(topics[1].TopicID, model.TopicID);
            Assert.AreEqual(topics[1].Category, model.Category);
        }
    }

}
