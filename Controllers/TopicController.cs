using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogApp.Models;

namespace BlogApp.Controllers
{
    public class TopicController : Controller
    {
        BlogRepository blogRepository = new BlogRepository();

        public ActionResult Blog(string topic, int id)
        {
            ViewBag.PageId = id;
            ViewBag.Topic = topic;
            ViewBag.Count = blogRepository.FindCount(topic);

            if (ViewBag.Count == 0)
            {
                return View("NotFound");
            }
            else
            {
                return View();
            }
        }


        //
        // GET: /Topic/

        public ActionResult Index()
        {
            var topics = blogRepository.GetAllTopics();
            return View(topics);
        }


    }
}
